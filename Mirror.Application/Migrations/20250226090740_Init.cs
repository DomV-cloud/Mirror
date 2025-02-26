using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mirror.Application.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Memories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reminder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserMemoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Memories_UserMemoryId",
                        column: x => x.UserMemoryId,
                        principalTable: "Memories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAchieved = table.Column<bool>(type: "bit", nullable: true),
                    TrackedDays = table.Column<double>(type: "float", nullable: false),
                    PercentageAchieved = table.Column<double>(type: "float", nullable: false),
                    ProgressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressGoals_Progresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "Progresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressSections_Progresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "Progresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressGoalMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgressGoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextMeasurementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressGoalMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressGoalMeasurements_ProgressGoals_ProgressGoalId",
                        column: x => x.ProgressGoalId,
                        principalTable: "ProgressGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgressSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgressColumnValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressDate_Day = table.Column<int>(type: "int", nullable: false),
                    ProgressDate_Month = table.Column<int>(type: "int", nullable: false),
                    ProgressDate_Year = table.Column<int>(type: "int", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressValues_ProgressSections_ProgressSectionId",
                        column: x => x.ProgressSectionId,
                        principalTable: "ProgressSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "SavedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89"), "john.doe@example.com", "John", "Doe", "hashedpassword123", new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(6853), null },
                    { new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a"), "jane.smith@example.com", "Jane", "Smith", "hashedpassword456", new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(6857), null }
                });

            migrationBuilder.InsertData(
                table: "Progresses",
                columns: new[] { "Id", "Description", "IsActive", "ProgressName", "SavedDate", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), "Training to Marathon", false, "Time", new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7032), null, new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a") },
                    { new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), "Cutting body fat", true, "Weight", new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7028), null, new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89") }
                });

            migrationBuilder.InsertData(
                table: "ProgressGoals",
                columns: new[] { "Id", "IsAchieved", "PercentageAchieved", "ProgressId", "SavedDate", "TrackedDays", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("514c4dc6-eddf-49c8-93d8-0c7f8f12cb45"), false, 47.0, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7062), 20.0, null },
                    { new Guid("66e37671-8ca7-4641-b68a-5077de60c800"), false, 63.0, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7058), 30.0, null }
                });

            migrationBuilder.InsertData(
                table: "ProgressSections",
                columns: new[] { "Id", "ProgressId", "SavedDate", "SectionName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1f17c260-9106-43cf-8d9b-bde8f040e4bb"), new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7082), "Body Weight Measurements", null },
                    { new Guid("b9a5221f-ef31-40b4-b64e-1d7c6b80a798"), new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7087), "Run Time Measurements", null }
                });

            migrationBuilder.InsertData(
                table: "ProgressGoalMeasurements",
                columns: new[] { "Id", "MeasurementDay", "NextMeasurementDate", "ProgressGoalId", "SavedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("5516bdd9-090e-4446-a10a-1d30337623fa"), "Tuesday", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("66e37671-8ca7-4641-b68a-5077de60c800"), new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7173), null },
                    { new Guid("e70aee1b-c8f3-42d3-957f-c98366ba2f4f"), "Monday", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("514c4dc6-eddf-49c8-93d8-0c7f8f12cb45"), new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7176), null }
                });

            migrationBuilder.InsertData(
                table: "ProgressValues",
                columns: new[] { "Id", "ProgressColumnValue", "ProgressDate_Day", "ProgressDate_Month", "ProgressDate_Year", "ProgressSectionId", "SavedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("6819a558-9385-451a-affe-a953c6ab3477"), "71", 6, 8, 2024, new Guid("1f17c260-9106-43cf-8d9b-bde8f040e4bb"), new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7145), null },
                    { new Guid("cb71483f-7e2f-4a18-a2f3-41dacca6506a"), "72", 10, 8, 2024, new Guid("b9a5221f-ef31-40b4-b64e-1d7c6b80a798"), new DateTime(2025, 2, 26, 9, 7, 40, 36, DateTimeKind.Utc).AddTicks(7149), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserMemoryId",
                table: "Images",
                column: "UserMemoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Memories_UserId",
                table: "Memories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_UserId",
                table: "Progresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressGoalMeasurements_ProgressGoalId",
                table: "ProgressGoalMeasurements",
                column: "ProgressGoalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgressGoals_ProgressId",
                table: "ProgressGoals",
                column: "ProgressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgressSections_ProgressId",
                table: "ProgressSections",
                column: "ProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressValues_ProgressSectionId",
                table: "ProgressValues",
                column: "ProgressSectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ProgressGoalMeasurements");

            migrationBuilder.DropTable(
                name: "ProgressValues");

            migrationBuilder.DropTable(
                name: "Memories");

            migrationBuilder.DropTable(
                name: "ProgressGoals");

            migrationBuilder.DropTable(
                name: "ProgressSections");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
