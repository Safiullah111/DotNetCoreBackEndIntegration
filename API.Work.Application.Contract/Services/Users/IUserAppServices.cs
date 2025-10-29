

using API.Work.Application.Contract.Common;

namespace API.Work.Application.Contract.Services.Users;

public interface IUserAppServices
{
    Task<ApiResponse<UserDto>> GetAsync(Guid id);

    Task<ApiResponse<List<UserDto>>> GetListAsync(GetUserListDto input);

    Task<ApiResponse<Guid>> CreateAsync(CreateUserDto input);

    Task<ApiResponse<bool>> UpdateAsync(Guid id, UpdateUserDto input);

    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}
