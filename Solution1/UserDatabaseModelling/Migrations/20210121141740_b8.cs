using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseModelling.Migrations
{
    public partial class b8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionPlanCells_ProductionPlanColumns_ProductionPlanColumnId",
                table: "ProductionPlanCells");

            migrationBuilder.DropIndex(
                name: "IX_ProductionPlanCells_ProductionPlanColumnId",
                table: "ProductionPlanCells");

            migrationBuilder.DropColumn(
                name: "ProductionPlanColumnId",
                table: "ProductionPlanCells");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionPlanCells_ProductionPlanColumns_ColumnId",
                table: "ProductionPlanCells",
                column: "ColumnId",
                principalTable: "ProductionPlanColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionPlanCells_ProductionPlanColumns_ColumnId",
                table: "ProductionPlanCells");

            migrationBuilder.AddColumn<int>(
                name: "ProductionPlanColumnId",
                table: "ProductionPlanCells",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlanCells_ProductionPlanColumnId",
                table: "ProductionPlanCells",
                column: "ProductionPlanColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionPlanCells_ProductionPlanColumns_ProductionPlanColumnId",
                table: "ProductionPlanCells",
                column: "ProductionPlanColumnId",
                principalTable: "ProductionPlanColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
