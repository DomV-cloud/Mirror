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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    ProgressColumnHead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressColumnValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                columns: new[] { "Id", "Date", "Description", "UserId" },
                values: new object[,]
                {
                    { new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2024, 10, 26, 19, 27, 7, 801, DateTimeKind.Local).AddTicks(4481), "Initial progress entry for Jane", new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a") },
                    { new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2024, 10, 26, 19, 27, 7, 801, DateTimeKind.Local).AddTicks(4341), "Initial progress entry for John", new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89") }
                });

            migrationBuilder.InsertData(
                table: "ProgressValues",
                columns: new[] { "Id", "ProgressColumnHead", "ProgressColumnValue", "ProgressId" },
                values: new object[,]
                {
                    { new Guid("e25bcc09-5585-4abf-bcce-da53394a85ba"), "Weight", "70", new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e") },
                    { new Guid("fe22bbf4-2920-4fc1-8e10-a5ad682e6281"), "BMI", "22", new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344") }
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
