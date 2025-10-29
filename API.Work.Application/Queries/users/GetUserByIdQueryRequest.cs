using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Users;
using MediatR;


namespace API.Work.Application.Contract.Requests;

public class GetUserByIdQueryRequest : IRequest<ApiResponse<UserDto>>
{
    public Guid UserId { get; set; }

    public GetUserByIdQueryRequest(Guid userId)
    {
        UserId = userId;
    }
}
