using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSync.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GitHub",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GitHub",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "AspNetUsers");
        }
    }
}
