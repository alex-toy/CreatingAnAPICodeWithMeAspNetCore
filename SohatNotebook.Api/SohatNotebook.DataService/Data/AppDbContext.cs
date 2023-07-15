using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SohatNotebook.Entities.DbSet;

namespace SohatNotebook.DataService.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().HasData(new User
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