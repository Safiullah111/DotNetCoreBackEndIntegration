using System.ComponentModel.DataAnnotations;


namespace API.Work.Application.Contract.Services.Users;

public class UserDto
{
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string UserEmail { get; set; } 

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    public bool IsActive { get; set; }
}
