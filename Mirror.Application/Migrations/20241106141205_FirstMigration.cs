using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mirror.Application.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
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
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    ProgressDate_Year = table.Column<int>(type: "int", nullable: false)
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
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89"), "john.doe@example.com", "John", "Doe", "hashedpassword123" },
                    { new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a"), "jane.smith@example.com", "Jane", "Smith", "hashedpassword456" }
                });

            migrationBuilder.InsertData(
                table: "Progresses",
                columns: new[] { "Id", "Description", "ProgressName", "UserId" },
                values: new object[,]
                {
                    { new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), "Training to Marathon", "Time", new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a") },
                    { new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), "Cutting body fat", "Weight", new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89") }
                });

            migrationBuilder.InsertData(
                table: "ProgressValues",
                columns: new[] { "Id", "ProgressColumnHead", "ProgressColumnValue", "ProgressDate_Day", "ProgressDate_Month", "ProgressDate_Year", "ProgressId" },
                values: new object[,]
                {
                    { new Guid("35cb12b5-9441-4c0f-8360-5b9d6082cda3"), "Weight", "71", 6, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e") },
                    { new Guid("44e76a73-bdbf-48ee-9b20-73a4082a2624"), "Time", "27:18", 20, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344") },
                    { new Guid("593b9f57-c5bc-4b92-b444-9ea14301b60c"), "Weight", "73", 12, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e") },
                    { new Guid("75b8809b-827f-4f1c-9fa4-d81b5eedcc57"), "Time", "24:05", 15, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344") },
                    { new Guid("aedc3655-901c-4689-9d9a-a3c0387e329b"), "Time", "25:17", 5, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344") },
                    { new Guid("c38f98c3-682d-49d0-8b20-d7b40e015a74"), "Weight", "72", 10, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e") },
                    { new Guid("ceafc64d-6248-43d2-904b-8f37b7885eae"), "Time", "26:18", 10, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344") }
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
                name: "ProgressValues");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
