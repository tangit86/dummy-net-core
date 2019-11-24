using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace WorkerProfileApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns : table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(maxLength: 120, nullable: true),
                        UID = table.Column<string>(maxLength: 50, nullable: true),
                        Address = table.Column<string>(nullable: true),
                        Point = table.Column<Point>(nullable: true)
                },
                constraints : table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns : table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                        Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints : table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProfileSkills",
                columns : table => new
                {
                    ProfileID = table.Column<int>(nullable: false),
                        SkillID = table.Column<int>(nullable: false)
                },
                constraints : table =>
                {
                    table.PrimaryKey("PK_ProfileSkills", x => new { x.ProfileID, x.SkillID });
                    table.ForeignKey(
                        name: "FK_ProfileSkills_Profiles_ProfileID",
                        column : x => x.ProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ID",
                        onDelete : ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileSkills_Skills_SkillID",
                        column : x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "ID",
                        onDelete : ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns : new [] { "ID", "Address", "Name", "Point", "UID" },
                values : new object[, ]
                { { 1, "10 Downing St, Westminster, London SW1A 2AA, UK", "Test Profiler1", (NetTopologySuite.Geometries.Point) new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (51.5033635 -0.1276248)"), "userX" }, { 2, "Canada Square, Canary Wharf, London E14, UK", "Test Profiler2", (NetTopologySuite.Geometries.Point) new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (51.5053154 -0.0168585)"), "userX" }, { 3, "Caledonian Pl, Edinburgh EH11, UK", "Test Profiler3", (NetTopologySuite.Geometries.Point) new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (55.9438541 -3.2191237)"), "userY" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns : new [] { "ID", "Name" },
                values : new object[, ]
                { { 1, "Waiter" }, { 2, "Cook" }, { 3, "Builder" }, { 4, "Software Dev" }
                });

            migrationBuilder.InsertData(
                table: "ProfileSkills",
                columns : new [] { "ProfileID", "SkillID" },
                values : new object[, ]
                { { 1, 1 }, { 1, 2 }, { 2, 2 }, { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_Name",
                table: "Profiles",
                column: "Name",
                unique : true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileSkills_SkillID",
                table: "ProfileSkills",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique : true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileSkills");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}