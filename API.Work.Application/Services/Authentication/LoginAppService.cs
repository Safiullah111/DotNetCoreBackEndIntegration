
using API.Work.Application.Configurations.GenericServices;
using API.Work.Application.Contract.Common;
using API.Work.Application.Contract.Common.Expections;
using API.Work.Application.Contract.Services.Authentication;
using API.Work.Application.Contract.Services.JwtSettings;
using API.Work.Application.Contract.Services.Users;
using API.Work.Application.Services.JwtSettings;
using API.Work.Domain.Configurations.GenericRepository;
using API.Work.Domain.Services.Users;
using API.Work.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace API.Work.Application.Services.Authenticatioin;

public class LoginAppService : AppServiceBase<User>, ILoginAppService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRepositoryBase<UserLogin> _userLoginRepository;
    private readonly IRepositoryBase<LoginFailure> _userLoginFailureRepsitory;
    private readonly IRepositoryBase<RefreshToken> _refreshTokenRepsitory;
    public LoginAppService(IUserRepository userRepository,
        IRepositoryBase<UserLogin> userLoginRepsitory,
        IRepositoryBase<LoginFailure> _userLoginFailure,
        IRepositoryBase<LoginFailure> userLoginFailureRepsitory,
        IJwtTokenGenerator jwtTokenGenerator,
        IRepositoryBase<RefreshToken> refreshTokenRepsitory) : base(userRepository)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userLoginRepository = userLoginRepsitory;
        _userLoginFailureRepsitory = userLoginFailureRepsitory;
        _refreshTokenRepsitory = refreshTokenRepsitory;
    }
    public async Task<ApiResponse<JwtToken>> LoginAsync(LoginRequestDto login)
    {
        var user = await _userRepository.GetUserAsync(login.Email);

        if (user == null)
        {
            LoggerAccessor.Logger.LogInformation("User login attempt. Email: {Email}", login.Email);
            throw new UserNotFoundException(user.UserEmail, APIWorkDomainCode.UserNotFound);
        }

        if (!VerifyPassword(login.Password, user.PasswordHash))
        {
            LoggerAccessor.Logger.LogInformation("User login attempt with invalid password. Email: {Email}", login.Email);
            user.AccessFailedCount++;
            await _userRepository.UpdateAsync(user);

            if (user.AccessFailedCount >= 5)
            {
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(15);
                await LogFailure(login.Email, "", "Account locked due to 5 failed attempts");
            }

            await LogFailure(login.Email, "", "Invalid Credentials");

             throw new InvalidCredentialsException(login.Email, APIWorkDomainCode.InvalidCredential);

            //return ApiResponse<JwtToken>.Fail(new ApiError { Code = APIWorkDomainErrorCode.InvalidCredential, Message = AuthApplicationErrorMessagesKeys.InvalidCredential });
        }

        if (!user.IsActive)
        {
            LoggerAccessor.Logger.LogCritical("Inactive user login attempt. Email: {Email}", login.Email);
            throw new UserInactiveException(login.Email, APIWorkDomainCode.UserInactive);
            //return ApiResponse<JwtToken>.Fail(new ApiError { Code = APIWorkDomainErrorCode.UserInactive, Message = AuthApplicationErrorMessagesKeys.UserInactive });
        }

        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
        {
            LoggerAccessor.Logger.LogWarning("Locked out user login attempt. Email: {Email}", login.Email);
            throw new AccountLockedException(login.Email, APIWorkDomainCode.AccountLocked);
            //return ApiResponse<JwtToken>.Fail(new ApiError { Code = APIWorkDomainErrorCode.AccountLocked, Message = AuthApplicationErrorMessagesKeys.AccountLocked });
        }

        // Successful login
        user.AccessFailedCount = 0;
        user.LockoutEnd = null;
        await LogAttempt(user, true, "", "");
        LoggerAccessor.Logger.LogInformation("Successful login. Email: {Email}", login.Email);

        var refreshToken = GenerateRefreshToken(user);
        JwtToken jwtToken = _jwtTokenGenerator.GenerateToken(user);
        await _refreshTokenRepsitory.AddAsync(refreshToken);

        return ApiResponse<JwtToken>.Ok(new JwtToken
        {
            token = jwtToken.token,
            expiry = jwtToken.expiry,
            refreshToken = refreshToken.Token
        });
    }


    private bool VerifyPassword(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
    private async Task LogAttempt(User user, bool success, string ip, string userAgent)
    {
        await _userLoginRepository.AddAsync(new UserLogin
        {
            UserId = user.Id,
            Email = user.UserEmail,
            IsSuccessful = success,
            IPAddress = ip,
            UserAgent = userAgent
        });
    }
    private async Task LogFailure(string email, string ip, string reason)
    {
        await _userLoginFailureRepsitory.AddAsync(new LoginFailure
        {
            Email = email,
            IPAddress = ip,
            Reason = reason
        });
    }
    private RefreshToken GenerateRefreshToken(User user)
    {
        return new RefreshToken
        {
            UserId = user.Id,
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
            ExpiryDate = DateTime.UtcNow.AddDays(7)
        };
    }

    public async Task<ApiResponse<JwtToken>> RefreshTokenAsync(RefreshTokenRequestDto requestDto)
    {
        var refreshToken = await (await _refreshTokenRepsitory.GetAll())
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == requestDto.Token);

        if (refreshToken == null)
            throw new RefershTokenException(requestDto.UserEmail ?? requestDto.UserName, APIWorkDomainCode.RefreshTokenNotFound);
            //return ApiResponse<JwtToken>.Fail(new ApiError() { Code = APIWorkDomainErrorCode.RefreshTokenNotFound, Message = AuthApplicationErrorMessagesKeys.RefreshTokenNotFound });


        if (refreshToken.IsRevoked)
            throw new RefershTokenException(requestDto.UserEmail ?? requestDto.UserName, APIWorkDomainCode.RefreshTokenRevoked);

        //return ApiResponse<JwtToken>.Fail(new ApiError() { Code = APIWorkDomainErrorCode.RefreshTokenRevoked, Message = AuthApplicationErrorMessagesKeys.RefreshTokenRevoked });

        if (refreshToken.ExpiryDate < DateTime.UtcNow)
            throw new RefershTokenException(requestDto.UserEmail ?? requestDto.UserName, APIWorkDomainCode.RefreshTokenExpired);

        //return ApiResponse<JwtToken>.Fail(new ApiError() { Code = APIWorkDomainErrorCode.RefreshTokenExpired, Message = AuthApplicationErrorMessagesKeys.RefreshTokenExpired });


        var user = refreshToken.User;

        // Generate new access token and refresh token
        var newJwtToken = _jwtTokenGenerator.GenerateToken(user);
        var newRefreshToken = GenerateRefreshToken(user);
        // Revoke old refresh token
        refreshToken.IsRevoked = true;

        // Save new refresh token
        await _refreshTokenRepsitory.AddAsync(newRefreshToken);

        return ApiResponse<JwtToken>.Ok(new JwtToken
        {
            token = newJwtToken.token,
            expiry = newJwtToken.expiry,
            refreshToken = newRefreshToken.Token
        });
    }

    public async Task<ApiResponse<JwtToken>> LogoutAsync(Guid userId)
    {
        var tokens = await (await _refreshTokenRepsitory.GetAll())
            .Where(rt => rt.UserId == userId && !rt.IsRevoked && rt.ExpiryDate > DateTime.UtcNow)
            .ToListAsync();
        if (!tokens.Any())
        {
            throw new UserNotFoundException(userId.ToString(),APIWorkDomainCode.UserNotFound);
            //return ApiResponse<JwtToken>.Fail(new ApiError() { Code = APIWorkDomainErrorCode.UserNotFound, Message = AuthApplicationErrorMessagesKeys.UserNotFound });
        }
        tokens.ForEach(t => t.IsRevoked = true);

        ///TODO: convert Into unit of work;
        ///
        foreach (var token in tokens)
        {
            await _refreshTokenRepsitory.UpdateAsync(token);
        }
        return ApiResponse<JwtToken>.Ok(null);
    }
}
