using Microsoft.EntityFrameworkCore.Migrations;

namespace JkBook.Migrations
{
    public partial class addedsbin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SBIN",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SBIN",
                table: "Books");
        }
    }
}
