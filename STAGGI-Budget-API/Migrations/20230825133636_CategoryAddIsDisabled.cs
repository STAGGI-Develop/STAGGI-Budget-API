using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STAGGI_Budget_API.Migrations
{
    public partial class CategoryAddIsDisabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Categories");
        }
    }
}
