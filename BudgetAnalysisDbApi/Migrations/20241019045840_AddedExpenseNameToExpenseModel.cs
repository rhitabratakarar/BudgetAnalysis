using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetAnalysisDbApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpenseNameToExpenseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpenseName",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseName",
                table: "Expenses");
        }
    }
}
