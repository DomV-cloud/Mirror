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

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.HasKey("Id");

                    b.ToTable("Images");
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

                    b.Property<bool?>("IsAchieved")
                        .HasColumnType("bit")
                        .HasAnnotation("Relational:JsonPropertyName", "isAchieved");

                    b.Property<double>("PercentageAchieved")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "percentageAchieved");

                    b.Property<string>("ProgressName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "progressName");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.Property<double>("TrackedDays")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "trackedDays");

                    b.Property<string>("TrackingProgressDay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "trackingProgressDay");

                    b.Property<DateTime?>("Updated")
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
                            PercentageAchieved = 63.0,
                            ProgressName = "Weight",
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4635),
                            TrackedDays = 0.0,
                            TrackingProgressDay = "Tuesday",
                            UserId = new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89")
                        },
                        new
                        {
                            Id = new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                            Description = "Training to Marathon",
                            PercentageAchieved = 47.0,
                            ProgressName = "Time",
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4644),
                            TrackedDays = 0.0,
                            TrackingProgressDay = "Thursday",
                            UserId = new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a")
                        });
                });

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("ProgressColumnHead")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "progressColumnHead");

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

                    b.Property<Guid>("ProgressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SavedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "saved");

                    b.HasKey("Id");

                    b.HasIndex("ProgressId");

                    b.ToTable("ProgressValues");

                    b.HasAnnotation("Relational:JsonPropertyName", "progressValues");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e55d5f7a-6039-4345-b9f2-3087baf65333"),
                            ProgressColumnHead = "Weight",
                            ProgressColumnValue = "71",
                            ProgressDate_Day = 6,
                            ProgressDate_Month = 8,
                            ProgressDate_Year = 2024,
                            ProgressId = new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4690)
                        },
                        new
                        {
                            Id = new Guid("45e2bfa7-40f9-4e83-9daa-0b06616e0816"),
                            ProgressColumnHead = "Weight",
                            ProgressColumnValue = "72",
                            ProgressDate_Day = 10,
                            ProgressDate_Month = 8,
                            ProgressDate_Year = 2024,
                            ProgressId = new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4697)
                        },
                        new
                        {
                            Id = new Guid("dd68daf0-6337-4627-901f-1972bf8dfcc3"),
                            ProgressColumnHead = "Weight",
                            ProgressColumnValue = "73",
                            ProgressDate_Day = 12,
                            ProgressDate_Month = 8,
                            ProgressDate_Year = 2024,
                            ProgressId = new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4702)
                        },
                        new
                        {
                            Id = new Guid("027f34b1-dbf7-488f-85d8-92461e926581"),
                            ProgressColumnHead = "Time",
                            ProgressColumnValue = "25:17",
                            ProgressDate_Day = 5,
                            ProgressDate_Month = 1,
                            ProgressDate_Year = 2024,
                            ProgressId = new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4724)
                        },
                        new
                        {
                            Id = new Guid("e335f539-b442-4d95-9c30-fef9ca436c4e"),
                            ProgressColumnHead = "Time",
                            ProgressColumnValue = "26:18",
                            ProgressDate_Day = 10,
                            ProgressDate_Month = 1,
                            ProgressDate_Year = 2024,
                            ProgressId = new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4728)
                        },
                        new
                        {
                            Id = new Guid("3581ae63-2216-4b05-9ebe-4ba47544881e"),
                            ProgressColumnHead = "Time",
                            ProgressColumnValue = "24:05",
                            ProgressDate_Day = 15,
                            ProgressDate_Month = 1,
                            ProgressDate_Year = 2024,
                            ProgressId = new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4732)
                        },
                        new
                        {
                            Id = new Guid("3cb7d356-463e-4364-bd99-935ae95ccba8"),
                            ProgressColumnHead = "Time",
                            ProgressColumnValue = "27:18",
                            ProgressDate_Day = 20,
                            ProgressDate_Month = 1,
                            ProgressDate_Year = 2024,
                            ProgressId = new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4736)
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
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4386)
                        },
                        new
                        {
                            Id = new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a"),
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            Password = "hashedpassword456",
                            SavedDate = new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4395)
                        });
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

            modelBuilder.Entity("Mirror.Domain.Entities.ProgressValue", b =>
                {
                    b.HasOne("Mirror.Domain.Entities.Progress", "Progress")
                        .WithMany("ProgressValue")
                        .HasForeignKey("ProgressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Progress");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.Progress", b =>
                {
                    b.Navigation("ProgressValue");
                });

            modelBuilder.Entity("Mirror.Domain.Entities.User", b =>
                {
                    b.Navigation("Progresses");
                });
#pragma warning restore 612, 618
        }
    }
}
