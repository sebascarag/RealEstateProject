using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RealEstate.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RealEstateSeedInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Active", "Address", "Birthday", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name", "Photo" },
                values: new object[,]
                {
                    { 1, true, "3071 Todds Lane San Antonio, TX 78205", new DateTime(1982, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109), "System", new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109), "Sheri D. Coffelt", "984bb1a4-46d8-4fbd-b9b1-fa78d395be7b.png" },
                    { 2, true, "1641 Despard Street Atlanta, GA 30309", new DateTime(1994, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109), "System", new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109), "Donnie K. Russell", "ffc8ea6a-10c6-4997-916c-55b20910158c.png" },
                    { 3, true, "1271 Hillcrest Circle Golden Valley, MN 55427", new DateTime(1986, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109), "System", new DateTime(2023, 7, 13, 15, 54, 15, 119, DateTimeKind.Utc).AddTicks(6109), "Jeff S. Nelson", "d73beddb-805f-47ca-baac-d9a2dbfb92ec.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
