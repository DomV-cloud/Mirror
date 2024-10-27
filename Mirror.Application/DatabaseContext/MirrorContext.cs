using Microsoft.EntityFrameworkCore;
using Mirror.Domain.Entities;

namespace Mirror.Application.DatabaseContext
{
    public class MirrorContext : DbContext
    {
        public MirrorContext(DbContextOptions<MirrorContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Progress> Progresses => Set<Progress>();
        public DbSet<ProgressValue> ProgressValues => Set<ProgressValue>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Progress>()
            .HasMany(p => p.ProgressValue)
            .WithOne(pv => pv.Progress)
            .HasForeignKey(pv => pv.ProgressId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("36165a94-1a9c-43dd-bf13-97a4e61e8b89"),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "hashedpassword123"
                },
                new User
                {
                    Id = Guid.Parse("6d3080d4-5dbf-4549-8ac1-77713785de2a"),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Password = "hashedpassword456"
                }
            );

            modelBuilder.Entity<Progress>().HasData(
                new Progress
                {
                    Id = Guid.Parse("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                    Description = "Initial progress entry for John",
                    Date = DateTime.Now,
                    UserId = Guid.Parse("36165a94-1a9c-43dd-bf13-97a4e61e8b89")
                },
                new Progress
                {
                    Id = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                    Description = "Initial progress entry for Jane",
                    Date = DateTime.Now,
                    UserId = Guid.Parse("6d3080d4-5dbf-4549-8ac1-77713785de2a")
                }
            );

            modelBuilder.Entity<ProgressValue>().HasData(
                new ProgressValue
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnHead = "Weight",
                    ProgressColumnValue = "70",
                    ProgressId = Guid.Parse("89e39006-abb0-4d6c-a045-e36a1aa4c62e")
                },
                new ProgressValue
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnHead = "BMI",
                    ProgressColumnValue = "22",
                    ProgressId = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344")
                }
            );
        }
    }
}
