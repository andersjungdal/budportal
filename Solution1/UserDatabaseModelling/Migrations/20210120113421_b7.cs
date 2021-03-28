using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseModelling.Migrations
{
    public partial class b7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionPlanColumns_ProductionPlans_ProductionPlanId",
                table: "ProductionPlanColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_RawBidCells_RawBidColumns_RawBidColumnId",
                table: "RawBidCells");

            migrationBuilder.DropForeignKey(
                name: "FK_RawBidColumns_RawBids_RawBidId",
                table: "RawBidColumns");

            migrationBuilder.DropIndex(
                name: "IX_RawBidColumns_RawBidId",
                table: "RawBidColumns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RawBidCells",
                table: "RawBidCells");

            migrationBuilder.DropIndex(
                name: "IX_RawBidCells_RawBidColumnId",
                table: "RawBidCells");

            migrationBuilder.DropIndex(
                name: "IX_ProductionPlanColumns_ProductionPlanId",
                table: "ProductionPlanColumns");

            migrationBuilder.DropColumn(
                name: "RawBidId",
                table: "RawBidColumns");

            migrationBuilder.DropColumn(
                name: "ColumnId",
                table: "RawBidCells");

            migrationBuilder.DropColumn(
                name: "ProductionPlanId",
                table: "ProductionPlanColumns");

            migrationBuilder.AddColumn<string>(
                name: "CollumName",
                table: "RawBidColumns",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RawBidPublicIdentifier",
                table: "RawBidColumns",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "RawBidColumnId",
                table: "RawBidCells",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductionPlanPublicIdentifier",
                table: "ProductionPlanColumns",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RawBidCells",
                table: "RawBidCells",
                columns: new[] { "RawBidColumnId", "Index" });

            migrationBuilder.AddForeignKey(
                name: "FK_RawBidCells_RawBidColumns_RawBidColumnId",
                table: "RawBidCells",
                column: "RawBidColumnId",
                principalTable: "RawBidColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RawBidCells_RawBidColumns_RawBidColumnId",
                table: "RawBidCells");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RawBidCells",
                table: "RawBidCells");

            migrationBuilder.DropColumn(
                name: "CollumName",
                table: "RawBidColumns");

            migrationBuilder.DropColumn(
                name: "RawBidPublicIdentifier",
                table: "RawBidColumns");

            migrationBuilder.DropColumn(
                name: "ProductionPlanPublicIdentifier",
                table: "ProductionPlanColumns");

            migrationBuilder.AddColumn<int>(
                name: "RawBidId",
                table: "RawBidColumns",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RawBidColumnId",
                table: "RawBidCells",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ColumnId",
                table: "RawBidCells",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductionPlanId",
                table: "ProductionPlanColumns",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RawBidCells",
                table: "RawBidCells",
                columns: new[] { "ColumnId", "Index" });

            migrationBuilder.CreateIndex(
                name: "IX_RawBidColumns_RawBidId",
                table: "RawBidColumns",
                column: "RawBidId");

            migrationBuilder.CreateIndex(
                name: "IX_RawBidCells_RawBidColumnId",
                table: "RawBidCells",
                column: "RawBidColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlanColumns_ProductionPlanId",
                table: "ProductionPlanColumns",
                column: "ProductionPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionPlanColumns_ProductionPlans_ProductionPlanId",
                table: "ProductionPlanColumns",
                column: "ProductionPlanId",
                principalTable: "ProductionPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RawBidCells_RawBidColumns_RawBidColumnId",
                table: "RawBidCells",
                column: "RawBidColumnId",
                principalTable: "RawBidColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RawBidColumns_RawBids_RawBidId",
                table: "RawBidColumns",
                column: "RawBidId",
                principalTable: "RawBids",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
