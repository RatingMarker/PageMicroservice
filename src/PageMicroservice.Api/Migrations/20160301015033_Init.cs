using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace PageMicroservice.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    SiteId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.SiteId);
                });
            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    PageId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    FoundDate = table.Column<DateTime>(nullable: true),
                    LastScanDate = table.Column<DateTime>(nullable: true),
                    SiteId = table.Column<int>(nullable: false),
                    SiteSiteId = table.Column<int>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.PageId);
                    table.ForeignKey(
                        name: "FK_Page_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Page_Site_SiteSiteId",
                        column: x => x.SiteSiteId,
                        principalTable: "Site",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Page");
            migrationBuilder.DropTable("Site");
        }
    }
}
