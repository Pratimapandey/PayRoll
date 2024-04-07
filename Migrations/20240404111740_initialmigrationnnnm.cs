using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollManagement.Migrations
{
    public partial class initialmigrationnnnm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryPayDeductionDetails_Employees_EmployeeId",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryPayDeductionDetails_SalaryPayDeductionDetails_SalaryPayDeductionDetailSalaryPayDetailId_SalaryPayDeductionDetailEmploy~",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryPayDetails_Employees_EmployeeId",
                table: "SalaryPayDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalaryPayDeductionDetails_EmployeeId",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalaryPayDeductionDetails_SalaryPayDeductionDetailSalaryPayDetailId_SalaryPayDeductionDetailEmployeeId_SalaryPayDeductionDet~",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalaryPayDeductionDetails_SalaryPayDetailId",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropColumn(
                name: "DeductionName",
                table: "SalaryPayDetails");

            migrationBuilder.DropColumn(
                name: "GrossSalary",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropColumn(
                name: "SalaryPayDeductionDetailEmployeeId",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropColumn(
                name: "SalaryPayDeductionDetailMonth",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropColumn(
                name: "SalaryPayDeductionDetailSalaryPayDetailId",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropColumn(
                name: "SalaryPayDeductionDetailYear",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.RenameColumn(
                name: "DeductedAmount",
                table: "SalaryPayDetails",
                newName: "NetSalary");

            migrationBuilder.RenameColumn(
                name: "NetSalary",
                table: "SalaryPayDeductionDetails",
                newName: "DeductedAmount");

            migrationBuilder.RenameColumn(
                name: "BaseSalaryAmount",
                table: "Employees",
                newName: "BaseSalary");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "SalaryPayDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BaseSalary",
                table: "SalaryPayDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "SalaryPayDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "SalaryPayDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DeductionName",
                table: "SalaryPayDeductionDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LeaveBalance",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LeaveRequest",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryPayDetails_Employees_EmployeeId",
                table: "SalaryPayDetails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryPayDetails_Employees_EmployeeId",
                table: "SalaryPayDetails");

            migrationBuilder.DropColumn(
                name: "BaseSalary",
                table: "SalaryPayDetails");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "SalaryPayDetails");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "SalaryPayDetails");

            migrationBuilder.DropColumn(
                name: "DeductionName",
                table: "SalaryPayDeductionDetails");

            migrationBuilder.DropColumn(
                name: "LeaveBalance",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LeaveRequest",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "NetSalary",
                table: "SalaryPayDetails",
                newName: "DeductedAmount");

            migrationBuilder.RenameColumn(
                name: "DeductedAmount",
                table: "SalaryPayDeductionDetails",
                newName: "NetSalary");

            migrationBuilder.RenameColumn(
                name: "BaseSalary",
                table: "Employees",
                newName: "BaseSalaryAmount");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "SalaryPayDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DeductionName",
                table: "SalaryPayDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "GrossSalary",
                table: "SalaryPayDeductionDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SalaryPayDeductionDetailEmployeeId",
                table: "SalaryPayDeductionDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryPayDeductionDetailMonth",
                table: "SalaryPayDeductionDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryPayDeductionDetailSalaryPayDetailId",
                table: "SalaryPayDeductionDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryPayDeductionDetailYear",
                table: "SalaryPayDeductionDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayDeductionDetails_EmployeeId",
                table: "SalaryPayDeductionDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayDeductionDetails_SalaryPayDeductionDetailSalaryPayDetailId_SalaryPayDeductionDetailEmployeeId_SalaryPayDeductionDet~",
                table: "SalaryPayDeductionDetails",
                columns: new[] { "SalaryPayDeductionDetailSalaryPayDetailId", "SalaryPayDeductionDetailEmployeeId", "SalaryPayDeductionDetailMonth", "SalaryPayDeductionDetailYear" });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayDeductionDetails_SalaryPayDetailId",
                table: "SalaryPayDeductionDetails",
                column: "SalaryPayDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryPayDeductionDetails_Employees_EmployeeId",
                table: "SalaryPayDeductionDetails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryPayDeductionDetails_SalaryPayDeductionDetails_SalaryPayDeductionDetailSalaryPayDetailId_SalaryPayDeductionDetailEmploy~",
                table: "SalaryPayDeductionDetails",
                columns: new[] { "SalaryPayDeductionDetailSalaryPayDetailId", "SalaryPayDeductionDetailEmployeeId", "SalaryPayDeductionDetailMonth", "SalaryPayDeductionDetailYear" },
                principalTable: "SalaryPayDeductionDetails",
                principalColumns: new[] { "SalaryPayDetailId", "EmployeeId", "Month", "Year" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryPayDetails_Employees_EmployeeId",
                table: "SalaryPayDetails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}
