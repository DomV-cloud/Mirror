using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mirror.Domain.Entities;
using Mirror.Domain.Enums.Progress;
using Mirror.Domain.Enums.UserMemory;

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
        public DbSet<ProgressSection> ProgressSections => Set<ProgressSection>();
        public DbSet<ProgressGoal> ProgressGoals => Set<ProgressGoal>();
        public DbSet<ProgressGoalMeasurement> ProgressGoalMeasurements => Set<ProgressGoalMeasurement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // **Konverze enumů na string**
            modelBuilder.Entity<ProgressGoalMeasurement>()
                .Property(e => e.MeasurementDay)
                .HasConversion(new EnumToStringConverter<MeasurementDay>());

            modelBuilder.Entity<UserMemory>()
                .Property(e => e.Reminder)
                .HasConversion(new EnumToStringConverter<Reminder>());

            // **Definování vztahů mezi entitami**

            // ProgressSection → ProgressValues (1:N)
            modelBuilder.Entity<ProgressSection>()
                .HasMany(s => s.ProgressValues)
                .WithOne(r => r.ProgressSection)
                .HasForeignKey(r => r.ProgressSectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Progress → ProgressSections (1:N)
            modelBuilder.Entity<Progress>()
                .HasMany(p => p.Sections)
                .WithOne(pv => pv.Progress)
                .HasForeignKey(pv => pv.ProgressId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserMemory → Images (1:N)
            modelBuilder.Entity<UserMemory>()
                .HasMany(um => um.Images)
                .WithOne(img => img.UserMemory)
                .HasForeignKey(img => img.UserMemoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Progress → ProgressGoal (1:1)
            modelBuilder.Entity<Progress>()
                .HasOne(p => p.Goal)
                .WithOne(pg => pg.Progress)
                .HasForeignKey<ProgressGoal>(pg => pg.ProgressId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProgressGoal → ProgressGoalMeasurement (1:1)
            modelBuilder.Entity<ProgressGoal>()
                .HasOne(pg => pg.Measurement)
                .WithOne(m => m.ProgressGoal)
                .HasForeignKey<ProgressGoalMeasurement>(m => m.ProgressGoalId)
                .OnDelete(DeleteBehavior.Cascade);

            // **Seed data**

            // Seed Users
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

            // Seed Progress
            modelBuilder.Entity<Progress>().HasData(
                new Progress
                {
                    Id = Guid.Parse("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                    ProgressName = "Weight",
                    Description = "Cutting body fat",
                    UserId = Guid.Parse("36165a94-1a9c-43dd-bf13-97a4e61e8b89"),
                    SavedDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Progress
                {
                    Id = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                    ProgressName = "Time",
                    Description = "Training to Marathon",
                    UserId = Guid.Parse("6d3080d4-5dbf-4549-8ac1-77713785de2a"),
                    SavedDate = DateTime.UtcNow
                }
            );

            // Seed ProgressGoal
            modelBuilder.Entity<ProgressGoal>().HasData(
                new ProgressGoal
                {
                    Id = Guid.Parse("66e37671-8ca7-4641-b68a-5077de60c800"),
                    ProgressId = Guid.Parse("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                    IsAchieved = false,
                    TrackedDays = 30,
                    PercentageAchieved = 63,
                    SavedDate = DateTime.UtcNow,
                },
                new ProgressGoal
                {
                    Id = Guid.Parse("514c4dc6-eddf-49c8-93d8-0c7f8f12cb45"),
                    ProgressId = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                    IsAchieved = false,
                    TrackedDays = 20,
                    PercentageAchieved = 47,
                    SavedDate = DateTime.UtcNow
                }
            );

            // Seed ProgressSections
            modelBuilder.Entity<ProgressSection>().HasData(
                new ProgressSection
                {
                    Id = Guid.Parse("1f17c260-9106-43cf-8d9b-bde8f040e4bb"),
                    ProgressId = Guid.Parse("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                    SectionName = "Body Weight Measurements"
                },
                new ProgressSection
                {
                    Id = Guid.Parse("b9a5221f-ef31-40b4-b64e-1d7c6b80a798"),
                    ProgressId = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                    SectionName = "Run Time Measurements"
                }
            );

            // Seed ProgressValues
            modelBuilder.Entity<ProgressValue>().HasData(
                new ProgressValue
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnValue = "71",
                    ProgressSectionId = Guid.Parse("1f17c260-9106-43cf-8d9b-bde8f040e4bb"),
                    ProgressDate_Day = 6,
                    ProgressDate_Month = 8,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                },
                new ProgressValue
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnValue = "72",
                    ProgressSectionId = Guid.Parse("b9a5221f-ef31-40b4-b64e-1d7c6b80a798"),
                    ProgressDate_Day = 10,
                    ProgressDate_Month = 8,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                }
            );

            // Seed ProgressGoalMeasurement
            modelBuilder.Entity<ProgressGoalMeasurement>().HasData(
                new ProgressGoalMeasurement
                {
                    Id = Guid.NewGuid(),
                    ProgressGoalId = Guid.Parse("66e37671-8ca7-4641-b68a-5077de60c800"), 
                    MeasurementDay = MeasurementDay.Tuesday,
                    NextMeasurementDate = new DateTime(2025, 3, 1),
                    SavedDate = DateTime.UtcNow
                },
                new ProgressGoalMeasurement
                {
                    Id = Guid.NewGuid(),
                    ProgressGoalId = Guid.Parse("514c4dc6-eddf-49c8-93d8-0c7f8f12cb45"),
                    MeasurementDay = MeasurementDay.Monday,
                    NextMeasurementDate = new DateTime(2025, 1, 15),
                    SavedDate = DateTime.UtcNow
                }
            );
        }
    }
}
