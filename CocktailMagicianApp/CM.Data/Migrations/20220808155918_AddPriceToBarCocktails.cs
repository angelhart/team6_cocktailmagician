using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class AddPriceToBarCocktails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "BarCocktails",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6dc0db8-408c-4aff-bf99-0d46efd31787"),
                column: "ConcurrencyStamp",
                value: "74289849-49f0-4903-b842-1f609831d0bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acde9ca2-de8c-45a0-ad81-3c3b05c8c90e"),
                column: "ConcurrencyStamp",
                value: "2e22ebf5-5d8b-4d0d-90f5-de2cd3929698");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
                column: "ConcurrencyStamp",
                value: "ad12f47c-e634-4358-97d3-2db374e219bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
                column: "ConcurrencyStamp",
                value: "e39f9768-47d0-498d-a3b2-54d73b1335f0");

            migrationBuilder.UpdateData(
                table: "BarCocktails",
                keyColumns: new[] { "CocktailId", "BarId" },
                keyValues: new object[] { new Guid("347e304b-03cd-414f-91b2-faed4fdb86e9"), new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd") },
                column: "Price",
                value: 2.34f);

            migrationBuilder.UpdateData(
                table: "BarCocktails",
                keyColumns: new[] { "CocktailId", "BarId" },
                keyValues: new object[] { new Guid("3f088822-fa2c-45f1-aa96-067f07aa04ea"), new Guid("0899e918-977c-44d5-a5cb-de9559ad822c") },
                column: "Price",
                value: 3.45f);

            migrationBuilder.UpdateData(
                table: "BarCocktails",
                keyColumns: new[] { "CocktailId", "BarId" },
                keyValues: new object[] { new Guid("a3fd2a00-52c4-4293-a184-6f448d008015"), new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd") },
                column: "Price",
                value: 1.23f);

            migrationBuilder.InsertData(
                table: "BarCocktails",
                columns: new[] { "CocktailId", "BarId", "Price" },
                values: new object[,]
                {
                    { new Guid("3f088822-fa2c-45f1-aa96-067f07aa04ea"), new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"), 1.34f },
                    { new Guid("a3fd2a00-52c4-4293-a184-6f448d008015"), new Guid("0899e918-977c-44d5-a5cb-de9559ad822c"), 4.56f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BarCocktails",
                keyColumns: new[] { "CocktailId", "BarId" },
                keyValues: new object[] { new Guid("3f088822-fa2c-45f1-aa96-067f07aa04ea"), new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd") });

            migrationBuilder.DeleteData(
                table: "BarCocktails",
                keyColumns: new[] { "CocktailId", "BarId" },
                keyValues: new object[] { new Guid("a3fd2a00-52c4-4293-a184-6f448d008015"), new Guid("0899e918-977c-44d5-a5cb-de9559ad822c") });

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BarCocktails");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6dc0db8-408c-4aff-bf99-0d46efd31787"),
                column: "ConcurrencyStamp",
                value: "b3b76498-7db8-44d8-9036-57cf839049d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acde9ca2-de8c-45a0-ad81-3c3b05c8c90e"),
                column: "ConcurrencyStamp",
                value: "b51edc11-8583-4509-822a-6e8db66e1e2e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
                column: "ConcurrencyStamp",
                value: "40c4fe01-ffa3-43ab-9829-b1ee6e9ca84f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
                column: "ConcurrencyStamp",
                value: "b2163f79-7a9c-4c90-9d46-e644a366f5e8");
        }
    }
}
