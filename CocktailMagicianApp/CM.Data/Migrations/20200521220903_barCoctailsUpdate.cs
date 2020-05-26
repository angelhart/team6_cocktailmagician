using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class barCoctailsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BarCocktails",
                table: "BarCocktails");

            migrationBuilder.DropIndex(
                name: "IX_BarCocktails_CocktailId",
                table: "BarCocktails");

            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Cocktails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recipe",
                table: "Cocktails",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarCocktails",
                table: "BarCocktails",
                columns: new[] { "CocktailId", "BarId" });

            migrationBuilder.CreateIndex(
                name: "IX_BarCocktails_BarId",
                table: "BarCocktails",
                column: "BarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BarCocktails",
                table: "BarCocktails");

            migrationBuilder.DropIndex(
                name: "IX_BarCocktails_BarId",
                table: "BarCocktails");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Cocktails");

            migrationBuilder.DropColumn(
                name: "Recipe",
                table: "Cocktails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarCocktails",
                table: "BarCocktails",
                columns: new[] { "BarId", "CocktailId" });

            migrationBuilder.CreateIndex(
                name: "IX_BarCocktails_CocktailId",
                table: "BarCocktails",
                column: "CocktailId");
        }
    }
}
