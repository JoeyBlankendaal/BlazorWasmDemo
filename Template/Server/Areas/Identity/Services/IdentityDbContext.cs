using Microsoft.EntityFrameworkCore;
using Template.Shared.Areas.Identity.Models;

namespace Template.Server.Areas.Identity.Services;

public interface IIdentityDbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleClaim> RoleClaims { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
}

public class IdentityDbContext : IIdentityDbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleClaim> RoleClaims { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }

    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Role>(etb =>
        {
            etb.HasKey(r => r.Id);
            etb.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
        });

        builder.Entity<RoleClaim>(etb =>
        {
            etb.HasKey(rc => rc.Id);
        });

        builder.Entity<User>(etb =>
        {
            etb.HasKey(u => u.Id);
            etb.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
        });

        builder.Entity<UserClaim>(etb =>
        {
            etb.HasKey(uc => uc.Id);
        });

        builder.Entity<UserLogin>(etb =>
        {
            etb.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
        });

        builder.Entity<UserRole>(etb =>
        {
            etb.HasKey(ur => new { ur.UserId, ur.RoleId });
        });

        builder.Entity<UserToken>(etb =>
        {
            etb.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
        });
    }
}
