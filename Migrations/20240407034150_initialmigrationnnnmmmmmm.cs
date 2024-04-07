using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollManagement.Migrations
{
    public partial class initialmigrationnnnmmmmmm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaryPayDeductionDetails",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.AddColumn<int>(
                name: "SalaryPayDeductionDetailId",
                table: "SalaryPayDeductionDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaryPayDeductionDetails",
                table: "SalaryPayDeductionDetails",
                columns: new[] { "SalaryPayDeductionDetailId", "EmployeeId", "Month", "Year" });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayDeductionDetails_SalaryPayDetailId",
                table: "SalaryPayDeductionDetails",
                column: "SalaryPayDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaryPayDeductionDetails",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalaryPayDeductionDetails_SalaryPayDetailId",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropColumn(
                name: "SalaryPayDeductionDetailId",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaryPayDeductionDetails",
                table: "SalaryPayDeductionDetails",
                columns: new[] { "SalaryPayDetailId", "EmployeeId", "Month", "Year" });
        }
    }
}
