using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PetRenameFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetPetFeature_pet_features_TestFeaturesId",
                table: "PetPetFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetPetFeature",
                table: "PetPetFeature");

            migrationBuilder.DropIndex(
                name: "IX_PetPetFeature_TestFeaturesId",
                table: "PetPetFeature");

            migrationBuilder.RenameColumn(
                name: "TestFeaturesId",
                table: "PetPetFeature",
                newName: "FeaturesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetPetFeature",
                table: "PetPetFeature",
                columns: new[] { "FeaturesId", "PetsId" });

            migrationBuilder.CreateIndex(
                name: "IX_PetPetFeature_PetsId",
                table: "PetPetFeature",
                column: "PetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetPetFeature_pet_features_FeaturesId",
                table: "PetPetFeature",
                column: "FeaturesId",
                principalTable: "pet_features",
                principalColumn: "pet_feature_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetPetFeature_pet_features_FeaturesId",
                table: "PetPetFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PetPetFeature",
                table: "PetPetFeature");

            migrationBuilder.DropIndex(
                name: "IX_PetPetFeature_PetsId",
                table: "PetPetFeature");

            migrationBuilder.RenameColumn(
                name: "FeaturesId",
                table: "PetPetFeature",
                newName: "TestFeaturesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PetPetFeature",
                table: "PetPetFeature",
                columns: new[] { "PetsId", "TestFeaturesId" });

            migrationBuilder.CreateIndex(
                name: "IX_PetPetFeature_TestFeaturesId",
                table: "PetPetFeature",
                column: "TestFeaturesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PetPetFeature_pet_features_TestFeaturesId",
                table: "PetPetFeature",
                column: "TestFeaturesId",
                principalTable: "pet_features",
                principalColumn: "pet_feature_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
