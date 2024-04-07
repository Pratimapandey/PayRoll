using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollManagement.Migrations
{
    public partial class initialmigrationnnnmmmmmmmm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveDays",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveDays",
                table: "LeaveRequests");
        }
    }
}
