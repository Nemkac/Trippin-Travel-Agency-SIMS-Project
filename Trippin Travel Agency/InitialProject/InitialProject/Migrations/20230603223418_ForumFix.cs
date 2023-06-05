using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitialProject.Migrations
{
    /// <inheritdoc />
    public partial class ForumFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComments_Forums_Forumid",
                table: "ForumComments");

            migrationBuilder.RenameColumn(
                name: "Forumid",
                table: "ForumComments",
                newName: "forumId");

            migrationBuilder.RenameIndex(
                name: "IX_ForumComments_Forumid",
                table: "ForumComments",
                newName: "IX_ForumComments_forumId");

            migrationBuilder.AlterColumn<int>(
                name: "forumId",
                table: "ForumComments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComments_Forums_forumId",
                table: "ForumComments",
                column: "forumId",
                principalTable: "Forums",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComments_Forums_forumId",
                table: "ForumComments");

            migrationBuilder.RenameColumn(
                name: "forumId",
                table: "ForumComments",
                newName: "Forumid");

            migrationBuilder.RenameIndex(
                name: "IX_ForumComments_forumId",
                table: "ForumComments",
                newName: "IX_ForumComments_Forumid");

            migrationBuilder.AlterColumn<int>(
                name: "Forumid",
                table: "ForumComments",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComments_Forums_Forumid",
                table: "ForumComments",
                column: "Forumid",
                principalTable: "Forums",
                principalColumn: "id");
        }
    }
}
