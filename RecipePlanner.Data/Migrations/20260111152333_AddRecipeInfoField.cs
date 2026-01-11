using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipePlanner.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeInfoField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "Recipes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Info",
                table: "Recipes");
        }
    }
}
