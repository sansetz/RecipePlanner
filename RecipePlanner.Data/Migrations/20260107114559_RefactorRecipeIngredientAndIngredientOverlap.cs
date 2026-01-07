using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipePlanner.Data.Migrations {
    /// <inheritdoc />
    public partial class RefactorRecipeIngredientAndIngredientOverlap : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            // 1) Nieuwe tabel maken (met Id PK + Quantity)
            migrationBuilder.CreateTable(
                name: "RecipeIngredients_New",
                columns: table => new {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),

                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitId = table.Column<int>(type: "INTEGER", nullable: false),

                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_RecipeIngredients_New", x => x.Id);

                    table.ForeignKey(
                        name: "FK_RecipeIngredients_New_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_RecipeIngredients_New_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);

                    table.ForeignKey(
                        name: "FK_RecipeIngredients_New_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // 2) Data kopiëren: NumberOfUnits -> Quantity
            migrationBuilder.Sql(
                """
                INSERT INTO RecipeIngredients_New (RecipeId, IngredientId, UnitId, Quantity)
                SELECT RecipeId, IngredientId, UnitId, NumberOfUnits
                FROM RecipeIngredients;
                """
            );

            // 3) Oude tabel verwijderen
            migrationBuilder.DropTable(name: "RecipeIngredients");

            // 4) Nieuwe tabel hernoemen naar originele naam
            migrationBuilder.RenameTable(
                name: "RecipeIngredients_New",
                newName: "RecipeIngredients");

            // 5) Indexen opnieuw aanmaken (incl unieke combi)
            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_UnitId",
                table: "RecipeIngredients",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId_IngredientId",
                table: "RecipeIngredients",
                columns: new[] { "RecipeId", "IngredientId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            // 1) Oude structuur terugmaken (composite key + NumberOfUnits)
            migrationBuilder.CreateTable(
                name: "RecipeIngredients_Old",
                columns: table => new {
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfUnits = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_RecipeIngredients_Old", x => new { x.RecipeId, x.IngredientId });

                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Old_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Old_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Old_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // 2) Data terugkopiëren: Quantity -> NumberOfUnits
            migrationBuilder.Sql(
                """
                INSERT INTO RecipeIngredients_Old (RecipeId, IngredientId, UnitId, NumberOfUnits)
                SELECT RecipeId, IngredientId, UnitId, Quantity
                FROM RecipeIngredients;
                """
            );

            // 3) Nieuwe tabel weg, oude terug onder originele naam
            migrationBuilder.DropTable(name: "RecipeIngredients");

            migrationBuilder.RenameTable(
                name: "RecipeIngredients_Old",
                newName: "RecipeIngredients");
        }
    }
}
