using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SohatNotebook.Entities.DbSet;
using SohatNotebook.Entities.Dto.Incoming;

namespace SohatNotebook.DataService.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public virtual DbSet<UserDb> Users { get; set; }
        public virtual DbSet<HealthDataDb> HealthData { get; set; }
        public virtual DbSet<RefreshTokenDb> RefreshTokens { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserDb> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserDb>().HasData(new UserDb
            {
                Id = Guid.NewGuid(),
                FirstName = "alex",
                LastName = "rea",
                Email = "alex@test.fr",
                Phone = "1234",
                DateOfBirth = DateTime.Today,
                Country = "france",
                Profession = "developper",
                Hobby = "coding"
            });
        }
    }
}