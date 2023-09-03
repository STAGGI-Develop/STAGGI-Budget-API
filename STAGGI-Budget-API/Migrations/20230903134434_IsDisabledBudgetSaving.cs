using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STAGGI_Budget_API.Migrations
{
    public partial class IsDisabledBudgetSaving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Savings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Budgets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Savings");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Budgets");
        }
    }
}
