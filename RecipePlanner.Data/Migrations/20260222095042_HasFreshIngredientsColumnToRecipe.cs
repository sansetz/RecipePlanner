using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipePlanner.Data.Migrations {
    /// <inheritdoc />
    public partial class HasFreshIngredientsColumnToRecipe : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<bool>(
                name: "NoFreshIngeredients",
                table: "Recipes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "NoFreshIngeredients",
                table: "Recipes");
        }
    }
}
