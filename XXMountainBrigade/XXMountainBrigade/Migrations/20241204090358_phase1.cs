using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XXMountainBrigade.Migrations
{
    /// <inheritdoc />
    public partial class phase1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCompany",
                columns: table => new
                {
                    CoyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCompany", x => x.CoyId);
                });

            migrationBuilder.CreateTable(
                name: "tblRanks",
                columns: table => new
                {
                    RankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RanName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRanks", x => x.RankId);
                });

            migrationBuilder.CreateTable(
                name: "tblPersonnel",
                columns: table => new
                {
                    PersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoyId = table.Column<int>(type: "int", nullable: false),
                    RankId = table.Column<int>(type: "int", nullable: false),
                    PersNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPersonnel", x => x.PersId);
                    table.ForeignKey(
                        name: "FK_tblPersonnel_tblCompany_CoyId",
                        column: x => x.CoyId,
                        principalTable: "tblCompany",
                        principalColumn: "CoyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblPersonnel_tblRanks_RankId",
                        column: x => x.RankId,
                        principalTable: "tblRanks",
                        principalColumn: "RankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblCompany",
                columns: new[] { "CoyId", "CoyName" },
                values: new object[,]
                {
                    { 1, "Company A" },
                    { 2, "Company B" }
                });

            migrationBuilder.InsertData(
                table: "tblRanks",
                columns: new[] { "RankId", "RanName" },
                values: new object[,]
                {
                    { 1, "Private" },
                    { 2, "Sergeant" }
                });

            migrationBuilder.InsertData(
                table: "tblPersonnel",
                columns: new[] { "PersId", "CoyId", "DateOfBirth", "FirstName", "LastName", "PermanentAddress", "PersNo", "RankId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1994, 12, 4, 14, 33, 57, 458, DateTimeKind.Local).AddTicks(2369), "John", "Doe", "123 Main St, City, Country", "P001", 1 },
                    { 2, 2, new DateTime(1999, 12, 4, 14, 33, 57, 459, DateTimeKind.Local).AddTicks(3514), "Jane", "Smith", "456 Oak St, City, Country", "P002", 2 },
                    { 3, 1, new DateTime(1996, 12, 4, 14, 33, 57, 459, DateTimeKind.Local).AddTicks(3528), "Michael", "Johnson", "789 Pine St, City, Country", "P003", 1 },
                    { 4, 2, new DateTime(1992, 12, 4, 14, 33, 57, 459, DateTimeKind.Local).AddTicks(3581), "Emily", "Williams", "101 Maple St, City, Country", "P004", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPersonnel_CoyId",
                table: "tblPersonnel",
                column: "CoyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPersonnel_RankId",
                table: "tblPersonnel",
                column: "RankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPersonnel");

            migrationBuilder.DropTable(
                name: "tblCompany");

            migrationBuilder.DropTable(
                name: "tblRanks");
        }
    }
}
