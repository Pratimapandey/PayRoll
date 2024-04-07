using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollManagement.Migrations
{
    public partial class initialmigrationn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Employees",
                newName: "BaseSalaryAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BaseSalaryAmount",
                table: "Employees",
                newName: "Salary");
        }
    }
}
