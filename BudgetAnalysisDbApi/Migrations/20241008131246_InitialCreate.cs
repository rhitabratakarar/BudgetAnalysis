using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetAnalysisDbApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    ExpenseType = table.Column<int>(type: "int", nullable: false),
                    ExpenseCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Months_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Months",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Years_YearId",
                        column: x => x.YearId,
                        principalTable: "Years",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_MonthId",
                table: "Expenses",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_YearId",
                table: "Expenses",
                column: "YearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropTable(
                name: "Years");
        }
    }
}
