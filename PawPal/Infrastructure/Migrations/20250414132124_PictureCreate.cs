using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PictureCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_picture_url",
                table: "users");

            migrationBuilder.DropColumn(
                name: "pictures_urls",
                table: "pets");

            migrationBuilder.CreateTable(
                name: "pictures",
                columns: table => new
                {
                    picture_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    source = table.Column<int>(type: "integer", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    path = table.Column<string>(type: "text", nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    pet_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pictures", x => x.picture_id);
                    table.ForeignKey(
                        name: "FK_pictures_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "pet_id");
                    table.ForeignKey(
                        name: "FK_pictures_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_pictures_pet_id",
                table: "pictures",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "IX_pictures_user_id",
                table: "pictures",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pictures");

            migrationBuilder.AddColumn<string>(
                name: "profile_picture_url",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "pictures_urls",
                table: "pets",
                type: "jsonb",
                nullable: true);
        }
    }
}
