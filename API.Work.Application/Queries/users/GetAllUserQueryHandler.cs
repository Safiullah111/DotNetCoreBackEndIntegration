using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Users;
using MediatR;


namespace API.Work.Application.Queries.users;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, ApiResponse<List<UserDto>>>
{
    public readonly IUserAppServices _userAppServices;
    public GetAllUserQueryHandler(IUserAppServices userAppServices)
    {
        _userAppServices = userAppServices;
    }
    public async Task<ApiResponse<List<UserDto>>> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
    {
        return await _userAppServices.GetListAsync(request.GetUserListDto);
    }
}
