using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollManagement.Migrations
{
    public partial class initialmigrationnnnmmmmmmm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveBalance",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LeaveRequest",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    LeaveRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.LeaveRequestId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

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
        }
    }
}
