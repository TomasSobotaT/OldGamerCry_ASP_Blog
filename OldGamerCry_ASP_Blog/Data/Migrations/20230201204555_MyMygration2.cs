using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OldGamerCry_ASP_Blog.Data.Migrations
{
    public partial class MyMygration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contain",
                table: "Article",
                newName: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Article",
                newName: "Contain");
        }
    }
}
