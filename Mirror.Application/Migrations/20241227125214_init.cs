using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mirror.Application.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    SetReminder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    IsAchieved = table.Column<bool>(type: "bit", nullable: true),
                    TrackedDays = table.Column<double>(type: "float", nullable: false),
                    TrackingProgressDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentageAchieved = table.Column<double>(type: "float", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserMemoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Memories_UserMemoryId",
                        column: x => x.UserMemoryId,
                        principalTable: "Memories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProgressValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgressColumnHead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgressColumnValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgressDate_Day = table.Column<int>(type: "int", nullable: false),
                    ProgressDate_Month = table.Column<int>(type: "int", nullable: false),
                    ProgressDate_Year = table.Column<int>(type: "int", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressValues_Progresses_ProgressId",
                        column: x => x.ProgressId,
                        principalTable: "Progresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "SavedDate" },
                values: new object[,]
                {
                    { new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89"), "john.doe@example.com", "John", "Doe", "hashedpassword123", new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(543) },
                    { new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a"), "jane.smith@example.com", "Jane", "Smith", "hashedpassword456", new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(556) }
                });

            migrationBuilder.InsertData(
                table: "Progresses",
                columns: new[] { "Id", "Description", "IsAchieved", "PercentageAchieved", "ProgressName", "SavedDate", "TrackedDays", "TrackingProgressDay", "Updated", "UserId" },
                values: new object[,]
                {
                    { new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), "Training to Marathon", null, 47.0, "Time", new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(939), 0.0, "Thursday", null, new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a") },
                    { new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), "Cutting body fat", null, 63.0, "Weight", new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(925), 0.0, "Tuesday", null, new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89") }
                });

            migrationBuilder.InsertData(
                table: "ProgressValues",
                columns: new[] { "Id", "ProgressColumnHead", "ProgressColumnValue", "ProgressDate_Day", "ProgressDate_Month", "ProgressDate_Year", "ProgressId", "SavedDate" },
                values: new object[,]
                {
                    { new Guid("18497a98-d29a-45fb-aa12-dd014086b0e1"), "Time", "25:17", 5, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(1099) },
                    { new Guid("3b67e133-25d4-4d9e-bfe0-8a8b983fd173"), "Weight", "72", 10, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(1047) },
                    { new Guid("44b00923-62b4-4826-8bd2-105aca7a5274"), "Time", "26:18", 10, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(1106) },
                    { new Guid("64a5d69e-1b00-4422-b6ed-de898f26c3d1"), "Time", "24:05", 15, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(1112) },
                    { new Guid("8590a4a9-c3de-4cfc-ab4f-80d4635615b0"), "Time", "27:18", 20, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(1119) },
                    { new Guid("d30167a4-690a-4d9d-8729-13bcde840233"), "Weight", "71", 6, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(1037) },
                    { new Guid("f0aa1b6d-f09e-4192-93bd-f3ed2a144ef8"), "Weight", "73", 12, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2024, 12, 27, 12, 52, 12, 843, DateTimeKind.Utc).AddTicks(1077) }
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
                name: "IX_ProgressValues_ProgressId",
                table: "ProgressValues",
                column: "ProgressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ProgressValues");

            migrationBuilder.DropTable(
                name: "Memories");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
