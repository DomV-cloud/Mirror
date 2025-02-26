﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mirror.Application.DatabaseContext;

#nullable disable

namespace Mirror.Application.Migrations
{
    [DbContext(typeof(MirrorContext))]
    partial class MirrorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mirror.Domain.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "content");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "imageContentType");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "imageName");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "filePath");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "updated");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "url");

                    b.Property<Guid?>("UserMemoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "memoryId");

                    b.HasKey("Id");

                    b.HasIndex("UserMemoryId");

                    b.ToTable("Images");

                    b.HasAnnotation("Relational:JsonPropertyName", "memoryImages");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.Progress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "isActive");

                    b.Property<string>("ProgressName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "progressName");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "updated");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Progresses");

                    b.HasAnnotation("Relational:JsonPropertyName", "progress");

                    b.HasData(
                        new
                        {
                            Id = new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                            Description = "Cutting body fat",
                            IsActive = true,
                            ProgressName = "Weight",
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7028),
                            UserId = new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89")
                        },
                        new
                        {
                            Id = new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                            Description = "Training to Marathon",
                            IsActive = false,
                            ProgressName = "Time",
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7032),
                            UserId = new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a")
                        });
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressGoal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<bool?>("IsAchieved")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "isAchieved");

                    b.Property<double>("PercentageAchieved")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "percentageAchieved");

                    b.Property<Guid>("ProgressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.Property<double>("TrackedDays")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "trackedDays");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "updated");

                    b.HasKey("Id");

                    b.HasIndex("ProgressId")
                        .IsUnique();

                    b.ToTable("ProgressGoals");

                    b.HasAnnotation("Relational:JsonPropertyName", "goal");

                    b.HasData(
                        new
                        {
                            Id = new Guid("66e37671-8ca7-4641-b68a-5077de60c800"),
                            IsAchieved = false,
                            PercentageAchieved = 63.0,
                            ProgressId = new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7058),
                            TrackedDays = 30.0
                        },
                        new
                        {
                            Id = new Guid("514c4dc6-eddf-49c8-93d8-0c7f8f12cb45"),
                            IsAchieved = false,
                            PercentageAchieved = 47.0,
                            ProgressId = new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7062),
                            TrackedDays = 20.0
                        });
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressGoalMeasurement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("MeasurementDay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "measurementDay");

                    b.Property<DateTime>("NextMeasurementDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "nextMeasurementDate");

                    b.Property<Guid>("ProgressGoalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "updated");

                    b.HasKey("Id");

                    b.HasIndex("ProgressGoalId")
                        .IsUnique();

                    b.ToTable("ProgressGoalMeasurements");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5516bdd9-090e-4446-a10a-1d30337623fa"),
                            MeasurementDay = "Tuesday",
                            NextMeasurementDate = new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProgressGoalId = new Guid("66e37671-8ca7-4641-b68a-5077de60c800"),
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7173)
                        },
                        new
                        {
                            Id = new Guid("e70aee1b-c8f3-42d3-957f-c98366ba2f4f"),
                            MeasurementDay = "Monday",
                            NextMeasurementDate = new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProgressGoalId = new Guid("514c4dc6-eddf-49c8-93d8-0c7f8f12cb45"),
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7176)
                        });
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressSection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<Guid>("ProgressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.Property<string>("SectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "sectionName");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "updated");

                    b.HasKey("Id");

                    b.HasIndex("ProgressId");

                    b.ToTable("ProgressSections");

                    b.HasAnnotation("Relational:JsonPropertyName", "sections");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1f17c260-9106-43cf-8d9b-bde8f040e4bb"),
                            ProgressId = new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7082),
                            SectionName = "Body Weight Measurements"
                        },
                        new
                        {
                            Id = new Guid("b9a5221f-ef31-40b4-b64e-1d7c6b80a798"),
                            ProgressId = new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7087),
                            SectionName = "Run Time Measurements"
                        });
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("ProgressColumnValue")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "column-value");

                    b.Property<int>("ProgressDate_Day")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "day");

                    b.Property<int>("ProgressDate_Month")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "month");

                    b.Property<int>("ProgressDate_Year")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "year");

                    b.Property<Guid>("ProgressSectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "updated");

                    b.HasKey("Id");

                    b.HasIndex("ProgressSectionId");

                    b.ToTable("ProgressValues");

                    b.HasAnnotation("Relational:JsonPropertyName", "progressValues");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6819a558-9385-451a-affe-a953c6ab3477"),
                            ProgressColumnValue = "71",
                            ProgressDate_Day = 6,
                            ProgressDate_Month = 8,
                            ProgressDate_Year = 2024,
                            ProgressSectionId = new Guid("1f17c260-9106-43cf-8d9b-bde8f040e4bb"),
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7145)
                        },
                        new
                        {
                            Id = new Guid("cb71483f-7e2f-4a18-a2f3-41dacca6506a"),
                            ProgressColumnValue = "72",
                            ProgressDate_Day = 10,
                            ProgressDate_Month = 8,
                            ProgressDate_Year = 2024,
                            ProgressSectionId = new Guid("b9a5221f-ef31-40b4-b64e-1d7c6b80a798"),
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7149)
                        });
                });

            modelBuilder.Entity("Mirror.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "firstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "LastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "password");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "updated");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89"),
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Password = "hashedpassword123",
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(6853)
                        },
                        new
                        {
                            Id = new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a"),
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            Password = "hashedpassword456",
                            SavedDate = new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(6857)
                        });
                });

            modelBuilder.Entity("Mirror.Domain.Entities.UserMemory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "description");

                    b.Property<string>("MemoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "memoryName");

                    b.Property<string>("Reminder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "reminder");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "updated");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "userId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Memories");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.Image", b =>
                {
                    b.HasOne("Mirror.Domain.Entities.UserMemory", "UserMemory")
                        .WithMany("Images")
                        .HasForeignKey("UserMemoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("UserMemory");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.Progress", b =>
                {
                    b.HasOne("Mirror.Domain.Entities.User", "User")
                        .WithMany("Progresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressGoal", b =>
                {
                    b.HasOne("Mirror.Domain.Entities.Progress", "Progress")
                        .WithOne("Goal")
                        .HasForeignKey("Mirror.Domain.Entities.ProgressGoal", "ProgressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Progress");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressGoalMeasurement", b =>
                {
                    b.HasOne("Mirror.Domain.Entities.ProgressGoal", "ProgressGoal")
                        .WithOne("Measurement")
                        .HasForeignKey("Mirror.Domain.Entities.ProgressGoalMeasurement", "ProgressGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProgressGoal");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressSection", b =>
                {
                    b.HasOne("Mirror.Domain.Entities.Progress", "Progress")
                        .WithMany("Sections")
                        .HasForeignKey("ProgressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Progress");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressValue", b =>
                {
                    b.HasOne("Mirror.Domain.Entities.ProgressSection", "ProgressSection")
                        .WithMany("ProgressValues")
                        .HasForeignKey("ProgressSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProgressSection");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.UserMemory", b =>
                {
                    b.HasOne("Mirror.Domain.Entities.User", "User")
                        .WithMany("Memories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.Progress", b =>
                {
                    b.Navigation("Goal")
                        .IsRequired();

                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressGoal", b =>
                {
                    b.Navigation("Measurement")
                        .IsRequired();
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressSection", b =>
                {
                    b.Navigation("ProgressValues");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.User", b =>
                {
                    b.Navigation("Memories");

                    b.Navigation("Progresses");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.UserMemory", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
