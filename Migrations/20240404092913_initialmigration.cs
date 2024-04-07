using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollManagement.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "SalaryDetails",
                columns: table => new
                {
                    SalaryDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryDetails", x => x.SalaryDetailId);
                    table.ForeignKey(
                        name: "FK_SalaryDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryPayDetails",
                columns: table => new
                {
                    SalaryPayDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeductionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeductedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPayDetails", x => x.SalaryPayDetailId);
                    table.ForeignKey(
                        name: "FK_SalaryPayDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "SalaryPayDeductionDetails",
                columns: table => new
                {
                    SalaryPayDetailId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    NetSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalaryPayDeductionDetailEmployeeId = table.Column<int>(type: "int", nullable: true),
                    SalaryPayDeductionDetailMonth = table.Column<int>(type: "int", nullable: true),
                    SalaryPayDeductionDetailSalaryPayDetailId = table.Column<int>(type: "int", nullable: true),
                    SalaryPayDeductionDetailYear = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPayDeductionDetails", x => new { x.SalaryPayDetailId, x.EmployeeId, x.Month, x.Year });
                    table.ForeignKey(
                        name: "FK_SalaryPayDeductionDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryPayDeductionDetails_SalaryPayDeductionDetails_SalaryPayDeductionDetailSalaryPayDetailId_SalaryPayDeductionDetailEmploy~",
                        columns: x => new { x.SalaryPayDeductionDetailSalaryPayDetailId, x.SalaryPayDeductionDetailEmployeeId, x.SalaryPayDeductionDetailMonth, x.SalaryPayDeductionDetailYear },
                        principalTable: "SalaryPayDeductionDetails",
                        principalColumns: new[] { "SalaryPayDetailId", "EmployeeId", "Month", "Year" });
                    table.ForeignKey(
                        name: "FK_SalaryPayDeductionDetails_SalaryPayDetails_SalaryPayDetailId",
                        column: x => x.SalaryPayDetailId,
                        principalTable: "SalaryPayDetails",
                        principalColumn: "SalaryPayDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryDetails_EmployeeId",
                table: "SalaryDetails",
                column: "EmployeeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPayDetails_EmployeeId",
                table: "SalaryPayDetails",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryDetails");

            migrationBuilder.DropTable(
                name: "SalaryPayDeductionDetails");

            migrationBuilder.DropTable(
                name: "SalaryPayDetails");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
