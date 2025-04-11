using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PetModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "breed",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "features",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "age_months",
                table: "pets",
                newName: "species");

            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "pets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "pet_features",
                columns: table => new
                {
                    pet_feature_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    feature = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pet_features", x => x.pet_feature_id);
                });

            migrationBuilder.CreateTable(
                name: "PetPetFeature",
                columns: table => new
                {
                    PetsId = table.Column<int>(type: "integer", nullable: false),
                    TestFeaturesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetPetFeature", x => new { x.PetsId, x.TestFeaturesId });
                    table.ForeignKey(
                        name: "FK_PetPetFeature_pet_features_TestFeaturesId",
                        column: x => x.TestFeaturesId,
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
                name: "IX_PetPetFeature_TestFeaturesId",
                table: "PetPetFeature",
                column: "TestFeaturesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetPetFeature");

            migrationBuilder.DropTable(
                name: "pet_features");

            migrationBuilder.DropColumn(
                name: "age",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "species",
                table: "pets",
                newName: "age_months");

            migrationBuilder.AddColumn<string>(
                name: "breed",
                table: "pets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "features",
                table: "pets",
                type: "jsonb",
                nullable: true);
        }
    }
}
