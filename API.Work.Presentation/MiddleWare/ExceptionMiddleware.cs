using API.Work.Application.Contract;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Common.Expections.BusinessExceptions;
using API.Work.Application.Contract.Localization;
using API.Work.Domain.Services.Users;
using API.Work.EntityFrameWork.Exceptions;
using API.Work.Presentation.MiddleWare;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IExceptionStatusCodeMapper _statusCodeMapper;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IExceptionStatusCodeMapper statusCodeMapper)
    {
        _next = next;
        _logger = logger;
        _statusCodeMapper = statusCodeMapper;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException bex)
        {
            _logger.LogWarning(bex, "Business exception occurred.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 403;

            var error = new ApiError
            {
                Code = bex.Code,
                Message = L.Get( bex.Code, bex.Message)
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(ApiResponse<string>.Fail(error)));
        }
        catch (RepositoryException rex)
        {
            _logger.LogWarning(rex, "Repository exception occurred.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 403;

            var error = new ApiError
            {
                Entity = rex.Message,
                //TODO:- In Future may use Logger and hidde the fully exception just show innerExpption as string;
                Details = new List<string>() { rex.InnerException.ToString()}
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(ApiResponse<string>.Fail(error)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorResponse = new ApiError
            {
                Code = "InternalServerError",
                Message = "An unexpected error occurred."
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(ApiResponse<string>.Fail(errorResponse)));
        }
    }
}
