using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class Local : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6dc0db8-408c-4aff-bf99-0d46efd31787"),
                column: "ConcurrencyStamp",
                value: "69695018-07b8-4e00-9ef8-4c270a4479e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acde9ca2-de8c-45a0-ad81-3c3b05c8c90e"),
                column: "ConcurrencyStamp",
                value: "a8244420-0bdf-4bef-a6bd-446c1cf218e7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
                column: "ConcurrencyStamp",
                value: "cd8cbd63-c78e-495e-b2c7-2574125418e3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
                column: "ConcurrencyStamp",
                value: "fc08e685-8d3c-4511-9649-6e2fff424220");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
                column: "ImagePath",
                value: "\\images\\Bars\\DefaultBar.png");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
                column: "ImagePath",
                value: "\\images\\Bars\\DefaultBar.png");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: new Guid("347e304b-03cd-414f-91b2-faed4fdb86e9"),
                column: "ImagePath",
                value: "\\images\\Cocktails\\DefaultCocktail.png");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: new Guid("3f088822-fa2c-45f1-aa96-067f07aa04ea"),
                column: "ImagePath",
                value: "\\images\\Cocktails\\DefaultCocktail.png");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: new Guid("a3fd2a00-52c4-4293-a184-6f448d008015"),
                column: "ImagePath",
                value: "\\images\\Cocktails\\DefaultCocktail.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6dc0db8-408c-4aff-bf99-0d46efd31787"),
                column: "ConcurrencyStamp",
                value: "fa0675ff-efd2-4c26-b38a-1d9936f6d2ab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acde9ca2-de8c-45a0-ad81-3c3b05c8c90e"),
                column: "ConcurrencyStamp",
                value: "dedf62d2-5298-4d59-8608-5ea62b5922b1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
                column: "ConcurrencyStamp",
                value: "d609c4cb-7683-49d7-a6ec-8a072d8e91ce");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
                column: "ConcurrencyStamp",
                value: "59ae2303-980d-4062-87b1-518b8a35143e");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"),
                column: "ImagePath",
                value: "/images/0899E918-977C-44D5-A5CB-DE9559AD822C-logo.png");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
                column: "ImagePath",
                value: "/images/9BDBF5E7-AD83-415C-B359-9FF5E2F0DEDD.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: new Guid("347e304b-03cd-414f-91b2-faed4fdb86e9"),
                column: "ImagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: new Guid("3f088822-fa2c-45f1-aa96-067f07aa04ea"),
                column: "ImagePath",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: new Guid("a3fd2a00-52c4-4293-a184-6f448d008015"),
                column: "ImagePath",
                value: null);
        }
    }
}
