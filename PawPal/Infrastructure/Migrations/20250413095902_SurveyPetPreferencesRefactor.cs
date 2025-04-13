using System.Collections.Generic;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SurveyPetPreferencesRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_surveys_surveys_pet_preferences_pet_preferences_id",
                table: "surveys");

            migrationBuilder.DropIndex(
                name: "IX_surveys_pet_preferences_id",
                table: "surveys");

            migrationBuilder.AlterColumn<List<PetSpecies>>(
                name: "preferred_species",
                table: "surveys_pet_preferences",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<PetSpecies>),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<List<PetSize>>(
                name: "preferred_sizes",
                table: "surveys_pet_preferences",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<PetSize>),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<List<PetGender>>(
                name: "preferred_genders",
                table: "surveys_pet_preferences",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<PetGender>),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<List<PetAge>>(
                name: "preferred_ages",
                table: "surveys_pet_preferences",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<PetAge>),
                oldType: "jsonb");

            migrationBuilder.AddColumn<int>(
                name: "survey_id",
                table: "surveys_pet_preferences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_surveys_pet_preferences_survey_id",
                table: "surveys_pet_preferences",
                column: "survey_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_surveys_pet_preferences_surveys_survey_id",
                table: "surveys_pet_preferences",
                column: "survey_id",
                principalTable: "surveys",
                principalColumn: "survey_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_surveys_pet_preferences_surveys_survey_id",
                table: "surveys_pet_preferences");

            migrationBuilder.DropIndex(
                name: "IX_surveys_pet_preferences_survey_id",
                table: "surveys_pet_preferences");

            migrationBuilder.DropColumn(
                name: "survey_id",
                table: "surveys_pet_preferences");

            migrationBuilder.AlterColumn<List<PetSpecies>>(
                name: "preferred_species",
                table: "surveys_pet_preferences",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<PetSpecies>),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<PetSize>>(
                name: "preferred_sizes",
                table: "surveys_pet_preferences",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<PetSize>),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<PetGender>>(
                name: "preferred_genders",
                table: "surveys_pet_preferences",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<PetGender>),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<PetAge>>(
                name: "preferred_ages",
                table: "surveys_pet_preferences",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<PetAge>),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_surveys_pet_preferences_id",
                table: "surveys",
                column: "pet_preferences_id");

            migrationBuilder.AddForeignKey(
                name: "FK_surveys_surveys_pet_preferences_pet_preferences_id",
                table: "surveys",
                column: "pet_preferences_id",
                principalTable: "surveys_pet_preferences",
                principalColumn: "survey_pet_preferences_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
