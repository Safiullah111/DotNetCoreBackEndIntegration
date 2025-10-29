using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.JwtSettings;
using MediatR;

namespace API.Work.Application.Commands.Authentication;

public class CreateLogOutCommand: IRequest<ApiResponse<JwtToken>>
{
    public Guid Id { get; set; }

    public CreateLogOutCommand(Guid id)
    {
        Id = id;
    }
}
