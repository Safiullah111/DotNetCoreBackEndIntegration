using API.Work.Application.Common.Mapping;
using API.Work.Application.Configurations.GenericServices;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Common.Expections;
using API.Work.Application.Contract.Services.Users;
using API.Work.Domain.Services.Users;
using API.Work.Domain.Shared;
using System.Net;

namespace API.Work.Application.Services.Users;

public class UserAppService : AppServiceBase<User>, IUserAppServices
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager _userManger;

    public UserAppService(IUserRepository userRepository, UserManager userManager) : base(userRepository)
    {
        _userRepository = userRepository;
        _userManger = userManager;
    }

    public async Task<ApiResponse<UserDto>> GetAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        var userDto = ObjectMapper.Mapper.Map<UserDto>(user);

        if (user == null)
        {
            throw new UserNotFoundException(id.ToString(), APIWorkDomainCode.UserNotFound);
        }

        return ApiResponse<UserDto>.Ok(userDto, APIWorkDomainCode.UserRetrievedSuccessfully);
    }

    public async Task<ApiResponse<List<UserDto>>> GetListAsync(GetUserListDto input)
    {
        var users = await _userRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);
        var getUser = ObjectMapper.Mapper.Map<List<UserDto>>(users);

        return ApiResponse<List<UserDto>>.Ok(getUser, APIWorkDomainCode.UserRetrievedSuccessfully);
    }

    public async Task<ApiResponse<Guid>> CreateAsync(CreateUserDto input)
    {
        var user = ObjectMapper.Mapper.Map<User>(input);
        var id = await _userManger.CreateAsync(user, input.PasswordHash);
        if (id == Guid.Empty)
        {
            throw new UserException(user.UserEmail ?? user.FirstName, APIWorkDomainCode.UserCreationFailed);
        }

        return ApiResponse<Guid>.Ok(id,APIWorkDomainCode.UserSuccessfullyCreated);
    }

    public async Task<ApiResponse<bool>> UpdateAsync(Guid id, UpdateUserDto input)
    {
        var updated = await _userManger.UpdateAsync(ObjectMapper.Mapper.Map<User>(input));
        if (!updated)
        {
            throw new UserException(id.ToString(), APIWorkDomainCode.UserUpdateFailed);
        }

        return ApiResponse<bool>.Ok(true, APIWorkDomainCode.UserSuccessfullyUpdated);
    }

    public new async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        bool deleted = await _userRepository.DeleteAsync(id);
        if (!deleted)
        {
            throw new UserException(id.ToString(),APIWorkDomainCode.UserDeletionFailed);

        }

        return ApiResponse<bool>.Ok(true, APIWorkDomainCode.UserDeletedSuccessfully);
    }
}
