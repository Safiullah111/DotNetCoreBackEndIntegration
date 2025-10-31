using API.Work.Domain.Services.Permissions;
using API.Work.Domain.Services.Roles;
using API.Work.Domain.Services.Users;
using API.Work.EntityFrameWork.Configurations;
using Microsoft.EntityFrameworkCore;

namespace API.Work.EntityFrameWork;

public static class DataSeeder
{
    public static async Task SeedAsync(APIWorkDbContext db)
    {
        await EnsureSuperAdminExits(db);
        await EnsurePermissionsExist(db);
        await EnsureRolesExist(db);
        await EnsureUserRolExists(db);
    }
    private static async Task EnsureSuperAdminExits(APIWorkDbContext db)
    {
        var dbUsers = await db.Users.FirstOrDefaultAsync(x => x.AccessLevel == AccessLevel.SuperAdmin);
        if (dbUsers == null)
        {
            await db.Users.AddAsync(new User(Guid.NewGuid(), "SuperAdmin", "SuperAdmin@gmail.com", "$2a$11$ZKNcLwO0Syt3.aBwWhhz9.VHJ80FRzpXIDPYtba25QwA3khCVI4HC", "SuperAdmin", "SuperAdmin", true,AccessLevel.SuperAdmin));
        }

    }
    private static async Task EnsureUserRolExists(APIWorkDbContext db)
    {
        var dbUsers = await db.Users.FirstOrDefaultAsync(x => x.AccessLevel == AccessLevel.SuperAdmin);
        if (dbUsers != null)
        {
            List<Role> dbRoles = await db.Roles.ToListAsync();
            List<UserRole> dbUserRoles = await db.UserRoles.Where(x => x.UserId == dbUsers.Id).ToListAsync();
            if (dbUserRoles == null)
            {
                Guid superAdminUserId = dbUsers.Id;
                await SaveUserRoleToDataBaseAsync(db, dbUsers, dbRoles);
            }
            else
            {
                var newRoles = await (from role in db.Roles
                                      join userRole in db.UserRoles
                                      on role.Id equals userRole.RoleId into userRoleGroup
                                      from userRole in userRoleGroup.DefaultIfEmpty()
                                      where userRole == null
                                      select role).ToListAsync();
                await SaveUserRoleToDataBaseAsync(db, dbUsers, newRoles);
            }
        }
        await db.SaveChangesAsync();
    }

    private static async Task SaveUserRoleToDataBaseAsync(APIWorkDbContext db, User dbUsers, List<Role> roles)
    {
        foreach (var item in roles)
        {
            await db.UserRoles.AddAsync(new UserRole(Guid.NewGuid(), dbUsers.Id, item.Id));
        }
    }

    private static async Task EnsureRolesExist(APIWorkDbContext db)
    {
        var dbRolesNames = await db.Roles.Select(x => x.Name).ToListAsync();
        if (dbRolesNames.Any())
        {
            var newRoles = Roles.AllRoles.Where(role => !dbRolesNames.Contains(role)).ToList();
            if (newRoles.Any())
            {
                await SaveRolesToDb(db, newRoles);
            }
            else
            {
                await SaveRolesToDb(db, Roles.AllRoles);
            }

        }
        else
        {
            await SaveRolesToDb(db, Roles.AllRoles);
        }
    }

    private static async Task EnsurePermissionsExist(APIWorkDbContext db)
    {
        var dbpermissionsNames = await db.Permissions.Select(x => x.Name).ToListAsync();

        if (db.Permissions.Any())
        {
            var dbPermissionNames = dbpermissionsNames;
            var newPermissions = Permissions.AllPermissions.Where(permission => !dbPermissionNames.Contains(permission)).ToList();

            await SavePermissionsToDb(db, newPermissions);
        }
        else
        {
            await SavePermissionsToDb(db, Permissions.AllPermissions);
        }
    }

    private static async Task SavePermissionsToDb(APIWorkDbContext db, List<string> permission)
    {
        foreach (var name in permission)
        {
            db.Permissions.Add(new Permission(Guid.NewGuid(), name));
        }
        await db.SaveChangesAsync();
    }

    private static async Task SaveRolesToDb(APIWorkDbContext db, List<string> roles)
    {
        foreach (var role in roles)
        {
            db.Roles.Add(new Role(Guid.NewGuid(), role, role));
        }
        await db.SaveChangesAsync();
        var dbUsers = await db.Users.Select(x => new { x.AccessLevel , x.Id} ).ToListAsync();

        var dbRolesAfter = await db.Roles.ToListAsync();
        var allPermissions = await db.Permissions.ToListAsync();
        foreach (Role r in dbRolesAfter)
        {
            foreach (Permission permission in allPermissions)
            {
                
                if(dbUsers.Any(x => x.AccessLevel == AccessLevel.SuperAdmin) || dbUsers.Any(x => x.AccessLevel == AccessLevel.SuperAdmin) || permission.Name.Contains(".View"))
                {
                    foreach (var item in dbUsers.Where(x => x.AccessLevel == AccessLevel.SuperAdmin || x.AccessLevel == AccessLevel.SuperAdmin).Select(x => x.Id).ToList())
                    {
                        db.RolePermissions.Add(new RolePermission(Guid.NewGuid(), r.Id, permission.Id, item));
                    }
                }
                if (permission.Name.Contains(".View") && !(dbUsers.Any(x => x.AccessLevel == AccessLevel.SuperAdmin) && dbUsers.Any(x => x.AccessLevel == AccessLevel.Admin)))
                {
                    if (!dbUsers.Any(x => x.AccessLevel == AccessLevel.SuperAdmin) && !dbUsers.Any(x => x.AccessLevel == AccessLevel.Admin))
                    {
                        foreach (var item in dbUsers.Where(x => x.AccessLevel != AccessLevel.SuperAdmin || x.AccessLevel != AccessLevel.Admin).Select(x => x.Id).ToList())
                        {
                            db.RolePermissions.Add(new RolePermission(Guid.NewGuid(), r.Id, permission.Id, item));

                        }
                    }

                }
            }
        }
        await db.SaveChangesAsync();
    }
}

