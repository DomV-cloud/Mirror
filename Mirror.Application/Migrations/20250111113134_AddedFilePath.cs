using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mirror.Application.Migrations
{
    /// <inheritdoc />
    public partial class AddedFilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("08a5c64b-bfd9-4568-aa0e-eb6b2d5eec04"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("3e385397-34bf-4514-9850-1e5a06af021a"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("42ad7ebc-fea8-4528-9dd7-c62f9ea1aa82"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("b2c6722f-eee6-43a4-9aa9-169c6ec446bc"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("c29c3ed5-4c24-4944-a0af-5aa643ec50c4"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("c7b59644-eae1-457e-bf70-085dff8e656b"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("f3bf7e9d-5199-4063-89b4-bb8d10797995"));

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ProgressValues",
                columns: new[] { "Id", "ProgressColumnHead", "ProgressColumnValue", "ProgressDate_Day", "ProgressDate_Month", "ProgressDate_Year", "ProgressId", "SavedDate" },
                values: new object[,]
                {
                    { new Guid("5728290d-19ef-492c-b5c0-88b5ae372601"), "Weight", "73", 12, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(8241) },
                    { new Guid("7c0a34ba-acf1-48d6-a9d7-ea03aeb628ed"), "Weight", "72", 10, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(8236) },
                    { new Guid("94e3550e-a874-4235-9692-2b2a2d61b549"), "Time", "26:18", 10, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(8263) },
                    { new Guid("9f8a668a-31ef-4f07-b0f4-ab3e13e03fb0"), "Time", "27:18", 20, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(8273) },
                    { new Guid("ee26e24d-47b9-4181-be70-425d95d230e6"), "Time", "25:17", 5, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(8259) },
                    { new Guid("f1d7ecba-22e5-474c-bcad-d8d520436a68"), "Time", "24:05", 15, 1, 2024, new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"), new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(8268) },
                    { new Guid("f8b69ace-2e35-4058-97cd-6578bff6f03d"), "Weight", "71", 6, 8, 2024, new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"), new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(8226) }
                });

            migrationBuilder.UpdateData(
                table: "Progresses",
                keyColumn: "Id",
                keyValue: new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                column: "SavedDate",
                value: new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(7918));

            migrationBuilder.UpdateData(
                table: "Progresses",
                keyColumn: "Id",
                keyValue: new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                column: "SavedDate",
                value: new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(7904));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89"),
                column: "SavedDate",
                value: new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(7459));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a"),
                column: "SavedDate",
                value: new DateTime(2025, 1, 11, 11, 31, 32, 553, DateTimeKind.Utc).AddTicks(7468));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("5728290d-19ef-492c-b5c0-88b5ae372601"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("7c0a34ba-acf1-48d6-a9d7-ea03aeb628ed"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("94e3550e-a874-4235-9692-2b2a2d61b549"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("9f8a668a-31ef-4f07-b0f4-ab3e13e03fb0"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("ee26e24d-47b9-4181-be70-425d95d230e6"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("f1d7ecba-22e5-474c-bcad-d8d520436a68"));

            migrationBuilder.DeleteData(
                table: "ProgressValues",
                keyColumn: "Id",
                keyValue: new Guid("f8b69ace-2e35-4058-97cd-6578bff6f03d"));

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Images");

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

            migrationBuilder.UpdateData(
                table: "Progresses",
                keyColumn: "Id",
                keyValue: new Guid("42f99827-ca6e-4f5b-a31f-a99458c2e344"),
                column: "SavedDate",
                value: new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5466));

            migrationBuilder.UpdateData(
                table: "Progresses",
                keyColumn: "Id",
                keyValue: new Guid("89e39006-abb0-4d6c-a045-e36a1aa4c62e"),
                column: "SavedDate",
                value: new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5458));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("36165a94-1a9c-43dd-bf13-97a4e61e8b89"),
                column: "SavedDate",
                value: new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5121));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6d3080d4-5dbf-4549-8ac1-77713785de2a"),
                column: "SavedDate",
                value: new DateTime(2025, 1, 3, 10, 17, 46, 263, DateTimeKind.Utc).AddTicks(5129));
        }
    }
}
