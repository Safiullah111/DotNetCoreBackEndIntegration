namespace API.Work.Domain.Services.Roles;

public class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string SuperAdmin = "SuperAdmin";
    public const string Manager = "Manager";

    public static List<string> AllRoles = new List<string> { Admin, User };
}
