using API.Work.Domain.Services.Permissions;
using API.Work.Domain.Services.Roles;
using API.Work.Domain.Services.Users;
using API.Work.Domain.Services.Users.UserPermissions;
using Microsoft.EntityFrameworkCore;


namespace API.Work.EntityFrameWork.Configurations;

public class APIWorkDbContext : DbContext
{
    public APIWorkDbContext(DbContextOptions<APIWorkDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<UserLogin> userLogins { get; set; }
    public DbSet<LoginFailure> loginFailures { get; set; }
    public DbSet<RefreshToken> refreshTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(x => x.HasMany(ur => ur.UserRoles).WithOne(u => u.User).HasForeignKey(ur => ur.UserId));
        modelBuilder.Entity<Role>(x => x.HasMany(ur => ur.UserRoles).WithOne(r => r.Role).HasForeignKey(ur => ur.RoleId));
        modelBuilder.Entity<Permission>(x => x.HasMany(rp => rp.RolePermissions).WithOne(p => p.Permission).HasForeignKey(rp => rp.PermissionId).OnDelete(DeleteBehavior.Restrict));
        modelBuilder.Entity<Permission>(x => x.HasMany(rp => rp.UserPermissions).WithOne(p => p.Permission).HasForeignKey(rp => rp.PermissionId).OnDelete(DeleteBehavior.Restrict));
        modelBuilder.Entity<UserRole>(x => x.HasKey(x => new {x.RoleId,x.UserId })) ;
        modelBuilder.Entity<UserPermission>(x => x.HasKey(x => new { x.UserId, x.PermissionId }));
        modelBuilder.Entity<RolePermission>(x => x.HasKey(x => new { x.RoleId, x.PermissionId }) );
        modelBuilder.Entity<User>().HasIndex(x => new { x.UserEmail , x.UserName }).IsUnique();
        modelBuilder.Entity<UserLogin>().HasOne(x => x.User).WithMany(x => x.UserLogins).HasForeignKey(x => x.UserId);
        modelBuilder.Entity<RefreshToken>().HasOne(x => x.User).WithMany(x => x.RefreshTokens).HasForeignKey(x => x.UserId);
    }
}
