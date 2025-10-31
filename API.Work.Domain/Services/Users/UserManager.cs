namespace API.Work.Domain.Services.Users;


// Search about Domain Which is like UserManager : Domain
public class UserManager
{
    private readonly IUserRepository _userRepository;
    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Guid> CreateAsync(User user, string password)
    {
        // Here you can add logic to hash the password and store the user securely
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                                 
        User newUser = new User(Guid.NewGuid(), user.UserName,user.UserEmail,user.PasswordHash,user.FirstName,user.LastName,user.IsActive,user.AccessLevel);
        return await _userRepository.AddAsync(newUser);

    }

    public Task<bool> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        // Implement password change logic here
        if (user.PasswordHash == BCrypt.Net.BCrypt.HashPassword(currentPassword))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> DeleteUserAsync(User user)
    {
        // Implement user deletion logic here
        // For demonstration, we'll just return true
        return Task.FromResult(true);
    }

    public Task<bool> UpdateAsync(User user)
    {
        // Implement user update logic here
        // For demonstration, we'll just return true
        return Task.FromResult(true);
    }

}
