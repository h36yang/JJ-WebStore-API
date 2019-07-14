using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.DataAccess.Migrations
{
    public partial class ModifyActiveFlagNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 7, 13, 23, 34, 25, 194, DateTimeKind.Unspecified).AddTicks(2747), new TimeSpan(0, -4, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 7, 13, 23, 34, 25, 194, DateTimeKind.Unspecified).AddTicks(2783), new TimeSpan(0, -4, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 7, 13, 23, 34, 25, 194, DateTimeKind.Unspecified).AddTicks(2789), new TimeSpan(0, -4, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 7, 13, 23, 34, 25, 194, DateTimeKind.Unspecified).AddTicks(2793), new TimeSpan(0, -4, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 7, 13, 23, 31, 9, 460, DateTimeKind.Unspecified).AddTicks(8972), new TimeSpan(0, -4, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 7, 13, 23, 31, 9, 460, DateTimeKind.Unspecified).AddTicks(9009), new TimeSpan(0, -4, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "UpdatedOn" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 7, 13, 23, 31, 9, 460, DateTimeKind.Unspecified).AddTicks(9015), new TimeSpan(0, -4, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 7, 13, 23, 31, 9, 460, DateTimeKind.Unspecified).AddTicks(9019), new TimeSpan(0, -4, 0, 0, 0)) });
        }
    }
}
