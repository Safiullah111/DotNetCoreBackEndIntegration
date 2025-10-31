using API.Work.Application.Contract.Common.Expections;
using API.Work.Application.Contract.Common.Expections.BusinessExceptions;

namespace API.Work.Presentation.MiddleWare;

public interface IExceptionStatusCodeMapper
{
    int Map(Exception ex);
}

public class ExceptionStatusCodeMapper : IExceptionStatusCodeMapper
{
    private static readonly Dictionary<Type, int> ExceptionStatusCodes = new()
    {
        { typeof(UserNotFoundException), StatusCodes.Status404NotFound },
        { typeof(InvalidCredentialsException), StatusCodes.Status401Unauthorized },
        { typeof(UserInactiveException), StatusCodes.Status403Forbidden },
        { typeof(AccountLockedException), StatusCodes.Status403Forbidden },
        { typeof(BusinessException), StatusCodes.Status400BadRequest }
    };

    public int Map(Exception ex)
    {
        return ExceptionStatusCodes.TryGetValue(ex.GetType(), out var code)
            ? code
            : StatusCodes.Status500InternalServerError;
    }
}
