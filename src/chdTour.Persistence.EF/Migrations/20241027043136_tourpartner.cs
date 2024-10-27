using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chdTour.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class tourpartner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TourPartners",
                table: "TourPartners");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TourPartners",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "TourPartners",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourPartners",
                table: "TourPartners",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TourPartners_TourId",
                table: "TourPartners",
                column: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TourPartners",
                table: "TourPartners");

            migrationBuilder.DropIndex(
                name: "IX_TourPartners_TourId",
                table: "TourPartners");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TourPartners");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "TourPartners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourPartners",
                table: "TourPartners",
                columns: new[] { "TourId", "PartnerId" });
        }
    }
}
