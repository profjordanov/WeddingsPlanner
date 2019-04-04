using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingsPlanner.Data.EntityFramework.Migrations
{
    public partial class AgencyBase64ImageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64Image",
                table: "Agencies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64Image",
                table: "Agencies");
        }
    }
}
