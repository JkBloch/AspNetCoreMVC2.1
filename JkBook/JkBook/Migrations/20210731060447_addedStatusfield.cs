using Microsoft.EntityFrameworkCore.Migrations;

namespace JkBook.Migrations
{
    public partial class addedStatusfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeStatus",
                table: "Employee",
                nullable: true,
                maxLength:20);

            migrationBuilder.Sql("ALTER TABLE employee  WITH CHECK ADD  CONSTRAINT " +
                "CHK_EmployeeStatus_Employee" +
                " CHECK  ((EmployeeStatus='Active' OR EmployeeStatus='Left' ))");

            migrationBuilder.Sql("ALTER TABLE employee  WITH CHECK ADD  CONSTRAINT " +
                "CHK_Gender_Employee" +
               " CHECK  ((Gender='Male' OR Gender='Female' ))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeStatus",
                table: "Employee");
        }
    }
}
