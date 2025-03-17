using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyCalendar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechnicalOfficer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalOfficer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalOfficerWorkshift",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkShift = table.Column<int>(type: "int", nullable: false),
                    Weekdays = table.Column<int>(type: "int", nullable: false),
                    TechnicalOfficerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalOfficerWorkshift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalOfficerWorkshift_TechnicalOfficer_TechnicalOfficerId",
                        column: x => x.TechnicalOfficerId,
                        principalTable: "TechnicalOfficer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalOfficerWorkshift_TechnicalOfficerId",
                table: "TechnicalOfficerWorkshift",
                column: "TechnicalOfficerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicalOfficerWorkshift");

            migrationBuilder.DropTable(
                name: "TechnicalOfficer");
        }
    }
}
