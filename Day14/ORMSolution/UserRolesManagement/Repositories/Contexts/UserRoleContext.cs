
using UserRolesManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace UserRolesManagement.Repositories.Contexts
{
    public class UserRoleContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _conString;

        public UserRoleContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _conString =
                this._configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        //Table Mapped DBSet Entities
        public DbSet<UserRole> UserRoles {get;set;}
        public DbSet<Role> Roles {get;set;}

        //Setup Connection String for Entity Framework
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(
                _conString ?? throw new InvalidOperationException("Connection string is null.")
            );
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
              modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId);
                entity.Property(e => e.RoleId);
                modelBuilder.Entity<UserRole>().ToTable("userroles");
            });
             modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
                modelBuilder.Entity<Role>().ToTable("roles");
            });
        }
    }
}