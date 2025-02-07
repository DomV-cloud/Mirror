﻿using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Progress>()
                .Property(e => e.TrackingProgressDay)
                .HasConversion(new EnumToStringConverter<TrackingProgressDays>());

            modelBuilder.Entity<UserMemory>()
                .Property(e => e.Reminder)
                .HasConversion(new EnumToStringConverter<Reminder>());

            modelBuilder.Entity<ProgressSection>()
                .HasMany(s => s.ProgressValues)
                .WithOne(r => r.ProgressSection)
                .HasForeignKey(r => r.ProgressSectionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Progress>()
                .HasMany(p => p.Sections)
                .WithOne(pv => pv.Progress)
                .HasForeignKey(pv => pv.ProgressId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserMemory>()
                .HasMany(um => um.Images)
                .WithOne(img => img.UserMemory)
                .HasForeignKey(img => img.UserMemoryId)
                .OnDelete(DeleteBehavior.Cascade);

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
                    PercentageAchieved = 63,
                    TrackingProgressDay = TrackingProgressDays.Tuesday,
                    SavedDate = DateTime.UtcNow,
                    IsActive = true
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

            modelBuilder.Entity<ProgressSection>().HasData(
                new ProgressSection
                {
                    Id = Guid.Parse("1f17c260-9106-43cf-8d9b-bde8f040e4bb"),
                    ProgressId = Guid.Parse("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                    SectionName = "Body Weight Measurements",
                    ProgressValues = []
                },
                new ProgressSection
                {
                    Id = Guid.Parse("b9a5221f-ef31-40b4-b64e-1d7c6b80a798"),
                    ProgressId = Guid.Parse("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                    SectionName = "Run Time Measurements",
                    ProgressValues = []
                }
            );

            modelBuilder.Entity<ProgressValue>().HasData(
                new ProgressValue
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnValue = "71",
                    ProgressSectionId = Guid.Parse("1f17c260-9106-43cf-8d9b-bde8f040e4bb"), // replace with actual ProgressSection GUID
                    ProgressDate_Day = 6,
                    ProgressDate_Month = 8,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                },
                new ProgressValue
                {
                    Id = Guid.NewGuid(),
                    ProgressColumnValue = "72",
                    ProgressSectionId = Guid.Parse("b9a5221f-ef31-40b4-b64e-1d7c6b80a798"), // replace with actual ProgressSection GUID
                    ProgressDate_Day = 10,
                    ProgressDate_Month = 8,
                    ProgressDate_Year = 2024,
                    SavedDate = DateTime.UtcNow
                },
                 new ProgressValue
                 {
                     Id = Guid.NewGuid(),
                     ProgressColumnValue = "25:17",
                     ProgressSectionId = Guid.Parse("b9a5221f-ef31-40b4-b64e-1d7c6b80a798"),
                     ProgressDate_Day = 6,
                     ProgressDate_Month = 8,
                     ProgressDate_Year = 2024,
                     SavedDate = DateTime.UtcNow
                 },
                 new ProgressValue
                 {
                     Id = Guid.NewGuid(),
                     ProgressColumnValue = "24:19",
                     ProgressSectionId = Guid.Parse("b9a5221f-ef31-40b4-b64e-1d7c6b80a798"),
                     ProgressDate_Day = 6,
                     ProgressDate_Month = 8,
                     ProgressDate_Year = 2024,
                     SavedDate = DateTime.UtcNow
                 }
            );
        }
    }
}
