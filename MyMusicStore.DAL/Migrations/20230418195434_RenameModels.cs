using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMusicStore.DAL.Migrations
{
    public partial class RenameModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "img",
                table: "Artists",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "img",
                table: "Albums",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Artists",
                newName: "img");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Albums",
                newName: "img");
        }
    }
}
