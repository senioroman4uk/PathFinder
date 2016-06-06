using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PathFinder.Security.Authentication.Models
{
    public class SecurityContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public SecurityContext() :
            base("PathFinder")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("Users").Property(p => p.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AppUserRole>().ToTable("UserRoles").HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<AppUserLogin>().ToTable("UserLogins").HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
            modelBuilder.Entity<AppUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<AppRole>().ToTable("Roles");
        }
    }
}
