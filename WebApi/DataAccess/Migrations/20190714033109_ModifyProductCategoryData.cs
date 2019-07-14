using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.DataAccess.Migrations
{
    public partial class ModifyProductCategoryData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "IsActive", "UpdatedOn" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 7, 13, 23, 31, 9, 460, DateTimeKind.Unspecified).AddTicks(8972), new TimeSpan(0, -4, 0, 0, 0)), true, new DateTimeOffset(new DateTime(2019, 7, 13, 23, 31, 9, 460, DateTimeKind.Unspecified).AddTicks(9009), new TimeSpan(0, -4, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "IsActive", "UpdatedOn" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 7, 13, 23, 31, 9, 460, DateTimeKind.Unspecified).AddTicks(9015), new TimeSpan(0, -4, 0, 0, 0)), true, new DateTimeOffset(new DateTime(2019, 7, 13, 23, 31, 9, 460, DateTimeKind.Unspecified).AddTicks(9019), new TimeSpan(0, -4, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "IsActive", "UpdatedOn" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 7, 13, 23, 22, 42, 301, DateTimeKind.Unspecified).AddTicks(9669), new TimeSpan(0, -4, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2019, 7, 13, 23, 22, 42, 301, DateTimeKind.Unspecified).AddTicks(9703), new TimeSpan(0, -4, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "IsActive", "UpdatedOn" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 7, 13, 23, 22, 42, 301, DateTimeKind.Unspecified).AddTicks(9766), new TimeSpan(0, -4, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2019, 7, 13, 23, 22, 42, 301, DateTimeKind.Unspecified).AddTicks(9771), new TimeSpan(0, -4, 0, 0, 0)) });
        }
    }
}
