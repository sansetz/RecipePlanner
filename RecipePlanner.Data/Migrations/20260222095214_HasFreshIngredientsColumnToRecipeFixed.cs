using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipePlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class HasFreshIngredientsColumnToRecipeFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoFreshIngeredients",
                table: "Recipes",
                newName: "NoFreshIngredients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NoFreshIngredients",
                table: "Recipes",
                newName: "NoFreshIngeredients");
        }
    }
}
