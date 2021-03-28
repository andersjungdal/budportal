using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseModelling.Migrations
{
    public partial class normalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Road",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "RoadenId",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZoneId",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductionPlanColumns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionPlanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionPlanColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionPlanColumns_ProductionPlans_ProductionPlanId",
                        column: x => x.ProductionPlanId,
                        principalTable: "ProductionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RawBidColumns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RawBidId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawBidColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RawBidColumns_RawBids_RawBidId",
                        column: x => x.RawBidId,
                        principalTable: "RawBids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Road = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionPlanCells",
                columns: table => new
                {
                    ColumnId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    ProductionPlanColumnId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionPlanCells", x => new { x.ColumnId, x.Index });
                    table.ForeignKey(
                        name: "FK_ProductionPlanCells_ProductionPlanColumns_ProductionPlanColumnId",
                        column: x => x.ProductionPlanColumnId,
                        principalTable: "ProductionPlanColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RawBidCells",
                columns: table => new
                {
                    ColumnId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Prize = table.Column<double>(nullable: false),
                    RawBidColumnId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawBidCells", x => new { x.ColumnId, x.Index });
                    table.ForeignKey(
                        name: "FK_RawBidCells_RawBidColumns_RawBidColumnId",
                        column: x => x.RawBidColumnId,
                        principalTable: "RawBidColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_RoadenId",
                table: "Companies",
                column: "RoadenId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ZoneId",
                table: "Companies",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlanCells_ProductionPlanColumnId",
                table: "ProductionPlanCells",
                column: "ProductionPlanColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlanColumns_ProductionPlanId",
                table: "ProductionPlanColumns",
                column: "ProductionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RawBidCells_RawBidColumnId",
                table: "RawBidCells",
                column: "RawBidColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_RawBidColumns_RawBidId",
                table: "RawBidColumns",
                column: "RawBidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Roads_RoadenId",
                table: "Companies",
                column: "RoadenId",
                principalTable: "Roads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Zones_ZoneId",
                table: "Companies",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Roads_RoadenId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Zones_ZoneId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "ProductionPlanCells");

            migrationBuilder.DropTable(
                name: "RawBidCells");

            migrationBuilder.DropTable(
                name: "Roads");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "ProductionPlanColumns");

            migrationBuilder.DropTable(
                name: "RawBidColumns");

            migrationBuilder.DropIndex(
                name: "IX_Companies_RoadenId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ZoneId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "RoadenId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Road",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ZipCode",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
