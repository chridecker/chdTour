using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chdTour.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    ScalaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeScalas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeScalas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    TourDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Pitches = table.Column<int>(type: "INTEGER", nullable: false),
                    GradeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TypeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourPartners",
                columns: table => new
                {
                    TourId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PartnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PersonObjId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPartners", x => new { x.TourId, x.PartnerId });
                    table.ForeignKey(
                        name: "FK_TourPartners_Persons_PersonObjId",
                        column: x => x.PersonObjId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourPartners_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourPartners_PersonObjId",
                table: "TourPartners",
                column: "PersonObjId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "GradeScalas");

            migrationBuilder.DropTable(
                name: "TourPartners");

            migrationBuilder.DropTable(
                name: "TourTypes");

            migrationBuilder.DropTable(
                name: "Tours");
        }
    }
}
