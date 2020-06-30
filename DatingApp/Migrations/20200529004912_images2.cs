using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Migrations
{
    public partial class images2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicCloudId",
                table: "UsersPhotos");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "UsersPhotos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "UsersPhotos");

            migrationBuilder.AddColumn<string>(
                name: "PublicCloudId",
                table: "UsersPhotos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
