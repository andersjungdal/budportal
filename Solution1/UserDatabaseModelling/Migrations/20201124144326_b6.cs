using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseModelling.Migrations
{
    public partial class b6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProduktionsPlanTemplate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "XmlTemplate",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "ProductionPlanTemplateId",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RawBidTemplateId",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "XmlTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicIdentifire = table.Column<Guid>(nullable: false),
                    XMLTemplate = table.Column<string>(type: "xml", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XmlTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ProductionPlanTemplateId",
                table: "Companies",
                column: "ProductionPlanTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_RawBidTemplateId",
                table: "Companies",
                column: "RawBidTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_XmlTemplates_ProductionPlanTemplateId",
                table: "Companies",
                column: "ProductionPlanTemplateId",
                principalTable: "XmlTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_XmlTemplates_RawBidTemplateId",
                table: "Companies",
                column: "RawBidTemplateId",
                principalTable: "XmlTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_XmlTemplates_ProductionPlanTemplateId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_XmlTemplates_RawBidTemplateId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "XmlTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ProductionPlanTemplateId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_RawBidTemplateId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProductionPlanTemplateId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "RawBidTemplateId",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "ProduktionsPlanTemplate",
                table: "Companies",
                type: "xml",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XmlTemplate",
                table: "Companies",
                type: "xml",
                nullable: true);
        }
    }
}
