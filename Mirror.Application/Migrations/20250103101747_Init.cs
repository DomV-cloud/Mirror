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
                    Reminder = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89"), "john.doe@example.com", "John", "Doe", "hashedpassword123", new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5121) },
                    { new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a"), "jane.smith@example.com", "Jane", "Smith", "hashedpassword456", new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5129) }
                });

            migrationBuilder.InsertData(
                table: "Progresses",
                columns: new[] { "Id", "Description", "IsAchieved", "PercentageAchieved", "ProgressName", "SavedDate", "TrackedDays", "TrackingProgressDay", "Updated", "UserId" },
                values: new object[,]
                {
                    { new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), "Training to Marathon", null, 47.0, "Time", new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5466), 0.0, "Thursday", null, new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a") },
                    { new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), "Cutting body fat", null, 63.0, "Weight", new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5458), 0.0, "Tuesday", null, new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89") }
                });

            migrationBuilder.InsertData(
                table: "ProgressValues",
                columns: new[] { "Id", "ProgressColumnHead", "ProgressColumnValue", "ProgressDate_Day", "ProgressDate_Month", "ProgressDate_Year", "ProgressId", "SavedDate" },
                values: new object[,]
                {
                    { new Guid("08a5c64b-bfd9-4568-aa0e-eb6b2d5eec04"), "Time", "24:05", 15, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5595) },
                    { new Guid("3e385397-34bf-4514-9850-1e5a06af021a"), "Weight", "72", 10, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5579) },
                    { new Guid("42ad7ebc-fea8-4528-9dd7-c62f9ea1aa82"), "Time", "27:18", 20, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5620) },
                    { new Guid("b2c6722f-eee6-43a4-9aa9-169c6ec446bc"), "Weight", "71", 6, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5573) },
                    { new Guid("c29c3ed5-4c24-4944-a0af-5aa643ec50c4"), "Time", "25:17", 5, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5587) },
                    { new Guid("c7b59644-eae1-457e-bf70-085dff8e656b"), "Time", "26:18", 10, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5591) },
                    { new Guid("f3bf7e9d-5199-4063-89b4-bb8d10797995"), "Weight", "73", 12, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5584) }
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
