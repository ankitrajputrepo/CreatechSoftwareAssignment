using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XX_MountainBrigade.Migrations
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
                    RankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRanks", x => x.RankId);
                });

            migrationBuilder.CreateTable(
                name: "tblRegiments",
                columns: table => new
                {
                    RegId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegimentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CoyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRegiments", x => x.RegId);
                    table.ForeignKey(
                        name: "FK_tblRegiments_tblCompany_CoyId",
                        column: x => x.CoyId,
                        principalTable: "tblCompany",
                        principalColumn: "CoyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPersonnel",
                columns: table => new
                {
                    PersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TypeOfPersonnel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoyId = table.Column<int>(type: "int", nullable: false),
                    RankId = table.Column<int>(type: "int", nullable: false),
                    RegimentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPersonnel", x => x.PersId);
                    table.ForeignKey(
                        name: "FK_tblPersonnel_tblCompany_CoyId",
                        column: x => x.CoyId,
                        principalTable: "tblCompany",
                        principalColumn: "CoyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPersonnel_tblRanks_RankId",
                        column: x => x.RankId,
                        principalTable: "tblRanks",
                        principalColumn: "RankId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPersonnel_tblRegiments_RegimentId",
                        column: x => x.RegimentId,
                        principalTable: "tblRegiments",
                        principalColumn: "RegId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "tblCompany",
                columns: new[] { "CoyId", "CoyName" },
                values: new object[,]
                {
                    { 1, "A Coy" },
                    { 2, "B Coy" },
                    { 3, "C Coy" }
                });

            migrationBuilder.InsertData(
                table: "tblRanks",
                columns: new[] { "RankId", "RankName" },
                values: new object[,]
                {
                    { 1, "Gunner" },
                    { 2, "Rfn" },
                    { 3, "Sepoy" },
                    { 4, "Ln Naik" },
                    { 5, "Havildar" },
                    { 6, "Sub Major" }
                });

            migrationBuilder.InsertData(
                table: "tblRegiments",
                columns: new[] { "RegId", "CoyId", "RegimentName" },
                values: new object[,]
                {
                    { 1, 1, "1st Mountain Regiment" },
                    { 2, 2, "2nd Mountain Regiment" },
                    { 3, 3, "3rd Mountain Regiment" }
                });

            migrationBuilder.InsertData(
                table: "tblPersonnel",
                columns: new[] { "PersId", "CoyId", "DateOfBirth", "FirstName", "LastName", "PermanentAddress", "PersNo", "RankId", "RegimentId", "TypeOfPersonnel" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1994, 12, 5, 8, 54, 49, 897, DateTimeKind.Local).AddTicks(543), "John", "Doe", "123 Main St, City, Country", "P001", 1, 1, "Regular" },
                    { 2, 2, new DateTime(1999, 12, 5, 8, 54, 49, 898, DateTimeKind.Local).AddTicks(1705), "Jane", "Smith", "456 Oak St, City, Country", "P002", 2, 2, "Regular" },
                    { 3, 1, new DateTime(1996, 12, 5, 8, 54, 49, 898, DateTimeKind.Local).AddTicks(1720), "Michael", "Johnson", "789 Pine St, City, Country", "P003", 1, 1, "Regular" },
                    { 4, 2, new DateTime(1992, 12, 5, 8, 54, 49, 898, DateTimeKind.Local).AddTicks(1724), "Emily", "Williams", "101 Maple St, City, Country", "P004", 2, 2, "Regular" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPersonnel_CoyId",
                table: "tblPersonnel",
                column: "CoyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPersonnel_RankId",
                table: "tblPersonnel",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPersonnel_RegimentId",
                table: "tblPersonnel",
                column: "RegimentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRegiments_CoyId",
                table: "tblRegiments",
                column: "CoyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPersonnel");

            migrationBuilder.DropTable(
                name: "tblRanks");

            migrationBuilder.DropTable(
                name: "tblRegiments");

            migrationBuilder.DropTable(
                name: "tblCompany");
        }
    }
}
