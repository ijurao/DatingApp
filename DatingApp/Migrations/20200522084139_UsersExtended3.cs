using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.Migrations
{
    public partial class UsersExtended3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersPhotos_Users_UserApplicationId",
                table: "UsersPhotos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "UserApplicationId",
                table: "UsersPhotos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersPhotos_Users_UserApplicationId",
                table: "UsersPhotos",
                column: "UserApplicationId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersPhotos_Users_UserApplicationId",
                table: "UsersPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "UserApplicationId",
                table: "UsersPhotos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UsersPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersPhotos_Users_UserApplicationId",
                table: "UsersPhotos",
                column: "UserApplicationId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
