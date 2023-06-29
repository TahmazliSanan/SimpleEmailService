using DataAccess.Entities.RoleEntity;
using DataAccess.Entities.UserEntity;
using Helpers.EncryptionMethods;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = 1,
                Name = "Admin",
                CreatedUserId = 1,
                CreatedAt = DateTime.UtcNow
            });

            modelBuilder.Entity<Role>().HasData(new Role()
            {
                Id = 2,
                Name = "User",
                CreatedUserId = 1,
                CreatedAt = DateTime.UtcNow
            });

            var salt = Encryption.GenerateSalt();
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                Username = "Admin",
                Email = "tahmazlisanan2022@gmail.com",
                Salt = salt,
                Hash = Encryption.GenerateHash("23042002Sanan", salt),
                RoleId = 1,
                CreatedUserId = 1,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}