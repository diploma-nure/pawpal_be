using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SurveyRenameUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_surveys_users_UserId",
                table: "surveys");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "surveys",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_surveys_UserId",
                table: "surveys",
                newName: "IX_surveys_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_surveys_users_user_id",
                table: "surveys",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_surveys_users_user_id",
                table: "surveys");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "surveys",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_surveys_user_id",
                table: "surveys",
                newName: "IX_surveys_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_surveys_users_UserId",
                table: "surveys",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
