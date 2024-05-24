using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateChannel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelName",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Messages",
                newName: "ChatUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatUserId",
                table: "Messages",
                column: "ChatUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatUsers_ChatUserId",
                table: "Messages",
                column: "ChatUserId",
                principalTable: "ChatUsers",
                principalColumn: "ChatUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatUsers_ChatUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatUserId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ChatUserId",
                table: "Messages",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "ChannelName",
                table: "Messages",
                type: "text",
                nullable: true);
        }
    }
}
