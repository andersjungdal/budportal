using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseModelling.Migrations
{
    public partial class b4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    AreaId = table.Column<int>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    PublicIdentifier = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    XmlString = table.Column<string>(type: "xml", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionPlans_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionPlans_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionPlans_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlans_AreaId",
                table: "ProductionPlans",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlans_CompanyId",
                table: "ProductionPlans",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlans_UserId",
                table: "ProductionPlans",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionPlans");
        }
    }
}
