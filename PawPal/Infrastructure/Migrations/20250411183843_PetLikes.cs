using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PetLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_pet_features_pet_feature_id",
                table: "pets_pet_features");

            migrationBuilder.DropForeignKey(
                name: "fk_pets_pet_features_pet_id",
                table: "pets_pet_features");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_test_entities",
                table: "test_entities");

            migrationBuilder.DropPrimaryKey(
                name: "pk_pets",
                table: "pets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_test_entities",
                table: "test_entities",
                column: "test_entity_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pets",
                table: "pets",
                column: "pet_id");

            migrationBuilder.CreateTable(
                name: "pet_likes",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    pet_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pet_likes", x => new { x.user_id, x.pet_id });
                    table.ForeignKey(
                        name: "FK_pet_likes_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "pet_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pet_likes_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pet_likes_pet_id",
                table: "pet_likes",
                column: "pet_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pets_pet_features_pet_feature_id",
                table: "pets_pet_features",
                column: "pet_feature_id",
                principalTable: "pet_features",
                principalColumn: "pet_feature_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pets_pet_features_pet_id",
                table: "pets_pet_features",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "pet_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pets_pet_features_pet_feature_id",
                table: "pets_pet_features");

            migrationBuilder.DropForeignKey(
                name: "FK_pets_pet_features_pet_id",
                table: "pets_pet_features");

            migrationBuilder.DropTable(
                name: "pet_likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_test_entities",
                table: "test_entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pets",
                table: "pets");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_test_entities",
                table: "test_entities",
                column: "test_entity_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_pets",
                table: "pets",
                column: "pet_id");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_pet_features_pet_feature_id",
                table: "pets_pet_features",
                column: "pet_feature_id",
                principalTable: "pet_features",
                principalColumn: "pet_feature_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_pets_pet_features_pet_id",
                table: "pets_pet_features",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "pet_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
