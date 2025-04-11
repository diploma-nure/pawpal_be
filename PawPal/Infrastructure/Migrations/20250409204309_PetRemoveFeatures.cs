using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PetRemoveFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "features",
                table: "pets");

            migrationBuilder.AddColumn<List<string>>(
                name: "pictures_urls",
                table: "pets",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pictures_urls",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "features",
                table: "pets",
                type: "text",
                nullable: true);
        }
    }
}
