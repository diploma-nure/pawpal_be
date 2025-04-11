using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PetConfigureFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetPetFeature");

            migrationBuilder.CreateTable(
                name: "pets_pet_features",
                columns: table => new
                {
                    pet_id = table.Column<int>(type: "integer", nullable: false),
                    pet_feature_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pets_pet_features", x => new { x.pet_id, x.pet_feature_id });
                    table.ForeignKey(
                        name: "fk_pets_pet_features_pet_feature_id",
                        column: x => x.pet_feature_id,
                        principalTable: "pet_features",
                        principalColumn: "pet_feature_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pets_pet_features_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pets_pet_features_pet_feature_id",
                table: "pets_pet_features",
                column: "pet_feature_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pets_pet_features");

            migrationBuilder.CreateTable(
                name: "PetPetFeature",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "integer", nullable: false),
                    PetsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetPetFeature", x => new { x.FeaturesId, x.PetsId });
                    table.ForeignKey(
                        name: "FK_PetPetFeature_pet_features_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "pet_features",
                        principalColumn: "pet_feature_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetPetFeature_pets_PetsId",
                        column: x => x.PetsId,
                        principalTable: "pets",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetPetFeature_PetsId",
                table: "PetPetFeature",
                column: "PetsId");
        }
    }
}
