using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class RolesSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarCocktails_Bars_BarId",
                table: "BarCocktails");

            migrationBuilder.DropForeignKey(
                name: "FK_BarCocktails_Cocktails_CocktailId",
                table: "BarCocktails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailComments",
                table: "CocktailComments");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CocktailComments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BarComments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailComments",
                table: "CocktailComments",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a6dc0db8-408c-4aff-bf99-0d46efd31787"), "3acc5e71-bf16-40e0-a9a3-7a30b02628bd", "Admin", "ADMIN" },
                    { new Guid("acde9ca2-de8c-45a0-ad81-3c3b05c8c90e"), "99c5fee4-bbf3-4b3c-b5ad-1db0698c9523", "Member", "MEMBER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
                column: "ConcurrencyStamp",
                value: "ba34caf3-d927-45f7-80c2-6a27de5ef690");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
                column: "ConcurrencyStamp",
                value: "f43f75d3-df00-4c5c-ba62-eef65cb445fe");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailComments_CocktailId",
                table: "CocktailComments",
                column: "CocktailId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarCocktails_Bars_BarId",
                table: "BarCocktails",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BarCocktails_Cocktails_CocktailId",
                table: "BarCocktails",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarCocktails_Bars_BarId",
                table: "BarCocktails");

            migrationBuilder.DropForeignKey(
                name: "FK_BarCocktails_Cocktails_CocktailId",
                table: "BarCocktails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailComments",
                table: "CocktailComments");

            migrationBuilder.DropIndex(
                name: "IX_CocktailComments_CocktailId",
                table: "CocktailComments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6dc0db8-408c-4aff-bf99-0d46efd31787"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("acde9ca2-de8c-45a0-ad81-3c3b05c8c90e"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CocktailComments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BarComments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailComments",
                table: "CocktailComments",
                columns: new[] { "CocktailId", "AppUserId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("98190af6-ba8e-44ff-8619-4d3b90040b5b"),
                column: "ConcurrencyStamp",
                value: "3bcb87fd-de3b-4b3c-8863-a29ce1b6736c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e355f8c4-ee01-4986-89bb-d1b56d17ae23"),
                column: "ConcurrencyStamp",
                value: "01c8e44c-ac2c-4655-8061-ebd0200d6fce");

            migrationBuilder.AddForeignKey(
                name: "FK_BarCocktails_Bars_BarId",
                table: "BarCocktails",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarCocktails_Cocktails_CocktailId",
                table: "BarCocktails",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
