using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Services.Users;
using MediatR;

namespace API.Work.Application.Queries.users;

public class GetAllUserQueryRequest : IRequest<ApiResponse<List<UserDto>>>
{
    public GetUserListDto GetUserListDto { get; set; }
    public GetAllUserQueryRequest(GetUserListDto getUserListDto) => GetUserListDto = getUserListDto;

}
