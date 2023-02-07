using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshFarmMarket.Migrations
{
    public partial class ImageNAboutme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aboutme",
                table: "AspNetUsers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aboutme",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "AspNetUsers");
        }
    }
}
