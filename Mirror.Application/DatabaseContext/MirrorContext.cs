using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mirror.Domain.Entities;
using Mirror.Domain.Enums.Progress;

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
        public DbSet<Image> Images => Set<Image>();
        public DbSet<UserMemory> Memories => Set<UserMemory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Progress>()
            .Property(e => e.TrackingProgressDay)
            .HasConversion(new EnumToStringConverter<TrackingProgressDays>());

            modelBuilder.Entity<Progress>()
                .HasMany(p => p.ProgressValue)
                .WithOne(pv => pv.Progress)
                .HasForeignKey(pv => pv.ProgressId)
                .OnDelete(DeleteBehavior.Cascade);

            // Data seeding
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("36165a94-1a9c-43dd-bf13-97a4e61e8b89"),
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "hashedpassword123",
                    SavedDate = DateTime.UtcNow
                },
                new User
                {
                    Id = Guid.Parse("6d3080d4-5dbf-4549-8ac1-77713785de2a"),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Password = "hashedpassword456",
                    SavedDate = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<Progress>().HasData(
                new Progress
                {
                    Id = Guid.Parse("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                    ProgressName = "Weight",
                    Description = "Cutting body fat",
                    UserId = Guid.Parse("36165a94-1a9c-43dd-bf13-97a4e61e8b89"),
                    PercentageAchieved = 63,
                    TrackingProgressDay = TrackingProgressDays.Tuesday,
                    SavedDate = DateTime.UtcNow
                },
                new Progress
                {
                    Id = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                    ProgressName = "Time",
                    Description = "Training to Marathon",
                    UserId = Guid.Parse("6d3080d4-5dbf-4549-8ac1-77713785de2a"),
                    PercentageAchieved = 47,
                    TrackingProgressDay = TrackingProgressDays.Thursday,
                    SavedDate = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<ProgressValue>().HasData(
                new
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnHead = "Weight",
                    ProgressColumnValue = "71",
                    ProgressId = Guid.Parse("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                    ProgressDate_Day = 6,
                    ProgressDate_Month = 8,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                },
                new
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnHead = "Weight",
                    ProgressColumnValue = "72",
                    ProgressId = Guid.Parse("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                    ProgressDate_Day = 10,
                    ProgressDate_Month = 8,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                },
                new
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnHead = "Weight",
                    ProgressColumnValue = "73",
                    ProgressId = Guid.Parse("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                    ProgressDate_Day = 12,
                    ProgressDate_Month = 8,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                },
                new
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnHead = "Time",
                    ProgressColumnValue = "25:17",
                    ProgressId = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                    ProgressDate_Day = 5,
                    ProgressDate_Month = 1,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                },
                new
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnHead = "Time",
                    ProgressColumnValue = "26:18",
                    ProgressId = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                    ProgressDate_Day = 10,
                    ProgressDate_Month = 1,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                },
                new
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnHead = "Time",
                    ProgressColumnValue = "24:05",
                    ProgressId = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                    ProgressDate_Day = 15,
                    ProgressDate_Month = 1,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                },
                new
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnHead = "Time",
                    ProgressColumnValue = "27:18",
                    ProgressId = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                    ProgressDate_Day = 20,
                    ProgressDate_Month = 1,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                }
            );
        }
    }
}
