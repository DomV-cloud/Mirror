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
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SavedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

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
                    { new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89"), "john.doe@example.com", "John", "Doe", "hashedpassword123", new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4386) },
                    { new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a"), "jane.smith@example.com", "Jane", "Smith", "hashedpassword456", new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4395) }
                });

            migrationBuilder.InsertData(
                table: "Progresses",
                columns: new[] { "Id", "Description", "IsAchieved", "PercentageAchieved", "ProgressName", "SavedDate", "TrackedDays", "TrackingProgressDay", "Updated", "UserId" },
                values: new object[,]
                {
                    { new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), "Training to Marathon", null, 47.0, "Time", new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4644), 0.0, "Thursday", null, new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a") },
                    { new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), "Cutting body fat", null, 63.0, "Weight", new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4635), 0.0, "Tuesday", null, new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89") }
                });

            migrationBuilder.InsertData(
                table: "ProgressValues",
                columns: new[] { "Id", "ProgressColumnHead", "ProgressColumnValue", "ProgressDate_Day", "ProgressDate_Month", "ProgressDate_Year", "ProgressId", "SavedDate" },
                values: new object[,]
                {
                    { new Guid("027f34b1-dbf7-488f-85d8-92461e926581"), "Time", "25:17", 5, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4724) },
                    { new Guid("3581ae63-2216-4b05-9ebe-4ba47544881e"), "Time", "24:05", 15, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4732) },
                    { new Guid("3cb7d356-463e-4364-bd99-935ae95ccba8"), "Time", "27:18", 20, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4736) },
                    { new Guid("45e2bfa7-40f9-4e83-9daa-0b06616e0816"), "Weight", "72", 10, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4697) },
                    { new Guid("dd68daf0-6337-4627-901f-1972bf8dfcc3"), "Weight", "73", 12, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4702) },
                    { new Guid("e335f539-b442-4d95-9c30-fef9ca436c4e"), "Time", "26:18", 10, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4728) },
                    { new Guid("e55d5f7a-6039-4345-b9f2-3087baf65333"), "Weight", "71", 6, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2024, 12, 24, 11, 53, 17, 636, DateTimeKind.Utc).AddTicks(4690) }
                });

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
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
