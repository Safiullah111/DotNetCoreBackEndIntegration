namespace API.Work.Domain.Services.Permissions;

public static class Permissions
{
    public static class Users
    {
        public const string Default = "Users";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string View = Default + ".View";

        public static readonly List<string> All = new List<string>
        {
            Create,
            Edit,
            Delete,
            View
        };
    }

    public static class Dashboard
    {
        public const string View = "Dashboard.View";
        public static readonly List<string> All = new List<string>
        {
            View
        };
    }
    public static readonly List<string> AllPermissions = Users.All.Concat(Dashboard.All).ToList();
}
