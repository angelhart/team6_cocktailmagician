using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class seedBar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6dc0db8-408c-4aff-bf99-0d46efd31787"),
                column: "ConcurrencyStamp",
                value: "7b5168b5-eb96-465f-bc4d-033770fff2c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acde9ca2-de8c-45a0-ad81-3c3b05c8c90e"),
                column: "ConcurrencyStamp",
                value: "30957372-b791-42c8-a3b8-87c7bd292964");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
                column: "ConcurrencyStamp",
                value: "aa9a0d88-643b-45ea-bef3-6b049e52e611");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
                column: "ConcurrencyStamp",
                value: "4d5df7e0-48ff-41b7-8bc3-65dba6ef1311");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
                column: "ImagePath",
                value: "~/images/9BDBF5E7-AD83-415C-B359-9FF5E2F0DEDD.jpg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
                column: "ImagePath",
                value: "~/images/0899E918-977C-44D5-A5CB-DE9559AD822C-logo.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6dc0db8-408c-4aff-bf99-0d46efd31787"),
                column: "ConcurrencyStamp",
                value: "9f2925da-1031-4e5a-b1c4-1b82cb3538f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acde9ca2-de8c-45a0-ad81-3c3b05c8c90e"),
                column: "ConcurrencyStamp",
                value: "a2d82895-70f6-4d5d-ad7b-403fdfce50a5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
                column: "ConcurrencyStamp",
                value: "d78a9d75-e36a-4052-981d-33ac5f35bb13");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
                column: "ConcurrencyStamp",
                value: "c1925453-9b40-4f27-a5a1-f7a207090967");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
                column: "ImagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
                column: "ImagePath",
                value: null);
        }
    }
}
