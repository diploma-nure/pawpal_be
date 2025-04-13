using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SurveyPetPreferencesRefactor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_surveys_pet_preferences_id",
                table: "surveys",
                column: "pet_preferences_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_surveys_surveys_pet_preferences_pet_preferences_id",
                table: "surveys",
                column: "pet_preferences_id",
                principalTable: "surveys_pet_preferences",
                principalColumn: "survey_pet_preferences_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_surveys_surveys_pet_preferences_pet_preferences_id",
                table: "surveys");

            migrationBuilder.DropIndex(
                name: "IX_surveys_pet_preferences_id",
                table: "surveys");

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
    }
}
