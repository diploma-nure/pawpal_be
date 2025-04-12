using System;
using System.Collections.Generic;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Survey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "survey_id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "surveys_owner_details",
                columns: table => new
                {
                    survey_owner_details_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    has_owned_pets_before = table.Column<bool>(type: "boolean", nullable: false),
                    understands_responsibility = table.Column<bool>(type: "boolean", nullable: false),
                    has_sufficient_financial_resources = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys_owner_details", x => x.survey_owner_details_id);
                });

            migrationBuilder.CreateTable(
                name: "surveys_pet_preferences",
                columns: table => new
                {
                    survey_pet_preferences_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    preferred_species = table.Column<List<PetSpecies>>(type: "jsonb", nullable: false),
                    preferred_sizes = table.Column<List<PetSize>>(type: "jsonb", nullable: false),
                    preferred_ages = table.Column<List<PetAge>>(type: "jsonb", nullable: false),
                    preferred_genders = table.Column<List<PetGender>>(type: "jsonb", nullable: false),
                    desired_activity_level = table.Column<int>(type: "integer", nullable: false),
                    ready_for_special_needs_pet = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys_pet_preferences", x => x.survey_pet_preferences_id);
                });

            migrationBuilder.CreateTable(
                name: "surveys_residence_details",
                columns: table => new
                {
                    survey_residence_details_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    place_of_residence = table.Column<int>(type: "integer", nullable: false),
                    has_safe_walking_area = table.Column<bool>(type: "boolean", nullable: false),
                    pets_allowed_at_residence = table.Column<int>(type: "integer", nullable: false),
                    has_other_pets = table.Column<bool>(type: "boolean", nullable: false),
                    has_small_children = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys_residence_details", x => x.survey_residence_details_id);
                });

            migrationBuilder.CreateTable(
                name: "surveys_pet_preferences_pet_features",
                columns: table => new
                {
                    survey_pet_preferences_id = table.Column<int>(type: "integer", nullable: false),
                    pet_feature_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys_pet_preferences_pet_features", x => new { x.survey_pet_preferences_id, x.pet_feature_id });
                    table.ForeignKey(
                        name: "FK_surveys_pet_preferences_pet_features_pet_feature_id",
                        column: x => x.pet_feature_id,
                        principalTable: "pet_features",
                        principalColumn: "pet_feature_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_surveys_pet_preferences_pet_features_survey_pet_preferences_id",
                        column: x => x.survey_pet_preferences_id,
                        principalTable: "surveys_pet_preferences",
                        principalColumn: "survey_pet_preferences_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "surveys",
                columns: table => new
                {
                    survey_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vacation_pet_care_plan = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    owner_details_id = table.Column<int>(type: "integer", nullable: false),
                    residence_details_id = table.Column<int>(type: "integer", nullable: false),
                    pet_preferences_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys", x => x.survey_id);
                    table.ForeignKey(
                        name: "FK_surveys_surveys_owner_details_owner_details_id",
                        column: x => x.owner_details_id,
                        principalTable: "surveys_owner_details",
                        principalColumn: "survey_owner_details_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_surveys_surveys_pet_preferences_pet_preferences_id",
                        column: x => x.pet_preferences_id,
                        principalTable: "surveys_pet_preferences",
                        principalColumn: "survey_pet_preferences_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_surveys_surveys_residence_details_residence_details_id",
                        column: x => x.residence_details_id,
                        principalTable: "surveys_residence_details",
                        principalColumn: "survey_residence_details_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_surveys_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_surveys_owner_details_id",
                table: "surveys",
                column: "owner_details_id");

            migrationBuilder.CreateIndex(
                name: "IX_surveys_pet_preferences_id",
                table: "surveys",
                column: "pet_preferences_id");

            migrationBuilder.CreateIndex(
                name: "IX_surveys_residence_details_id",
                table: "surveys",
                column: "residence_details_id");

            migrationBuilder.CreateIndex(
                name: "IX_surveys_UserId",
                table: "surveys",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_surveys_pet_preferences_pet_features_pet_feature_id",
                table: "surveys_pet_preferences_pet_features",
                column: "pet_feature_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "surveys");

            migrationBuilder.DropTable(
                name: "surveys_pet_preferences_pet_features");

            migrationBuilder.DropTable(
                name: "surveys_owner_details");

            migrationBuilder.DropTable(
                name: "surveys_residence_details");

            migrationBuilder.DropTable(
                name: "surveys_pet_preferences");

            migrationBuilder.DropColumn(
                name: "survey_id",
                table: "users");
        }
    }
}
