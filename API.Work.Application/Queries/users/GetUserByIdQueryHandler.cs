using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Requests;
using API.Work.Application.Contract.Services.Users;
using MediatR;


namespace API.Work.Application.Queries.users;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, ApiResponse<UserDto>>
{
    private readonly IUserAppServices _userAppServices;

    public GetUserByIdQueryHandler(IUserAppServices userAppServices)
    {
        _userAppServices = userAppServices;
    }

    public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
    {
        return await _userAppServices.GetAsync(request.UserId);
    }
}
