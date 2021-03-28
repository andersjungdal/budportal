using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseModelling.Migrations
{
    public partial class b5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProduktionsPlanTemplate",
                table: "Companies",
                type: "xml",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProduktionsPlanTemplate",
                table: "Companies");
        }
    }
}
