using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.EF.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dices",
                columns: table => new
                {
                    NbFaces = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dices", x => x.NbFaces);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "throws",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Result = table.Column<int>(type: "INTEGER", nullable: false),
                    DiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_throws", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "dices",
                column: "NbFaces",
                values: new object[]
                {
                    2,
                    3,
                    4,
                    5,
                    6
                });

            migrationBuilder.InsertData(
                table: "profiles",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("98b76d55-e413-4ad0-97ab-bed373a13a65"), "Grienenberger", "Côme" },
                    { new Guid("b5dab26f-6975-483c-8bb6-1cd9b19190a0"), "Perret", "Louis" },
                    { new Guid("bd68c485-c883-44c5-a235-9190d3049d4a"), "Malvezin", "Neitah" }
                });

            migrationBuilder.InsertData(
                table: "throws",
                columns: new[] { "Id", "DiceId", "Result" },
                values: new object[,]
                {
                    { new Guid("003a02ce-db1e-405b-88a9-a6807f591c14"), 2, 2 },
                    { new Guid("0a7aa0d4-efac-467e-b151-e6dc17facbd0"), 3, 3 },
                    { new Guid("0a8e177e-fbd2-4ba5-80d3-506f22709592"), 4, 4 },
                    { new Guid("1c786418-3d11-4947-8901-53b35ac76502"), 6, 5 },
                    { new Guid("825e122e-897f-4a59-93be-c8c554a86b53"), 2, 1 },
                    { new Guid("ed12df19-cf27-4e14-9f7c-bb3d2774a52d"), 4, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dices");

            migrationBuilder.DropTable(
                name: "profiles");

            migrationBuilder.DropTable(
                name: "throws");
        }
    }
}
