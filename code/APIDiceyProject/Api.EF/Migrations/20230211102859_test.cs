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
                    DiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_throws", x => x.Id);
                    table.ForeignKey(
                        name: "FK_throws_profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { new Guid("15a4a021-bea3-45e5-a8ff-58d6a5d902cd"), "Grienenberger", "Côme" },
                    { new Guid("5a3888f4-8bcf-4002-a86a-461515a4dd89"), "Malvezin", "Neitah" },
                    { new Guid("cc6f9111-b174-4064-814b-ce7eb4169e80"), "Perret", "Louis" }
                });

            migrationBuilder.InsertData(
                table: "throws",
                columns: new[] { "Id", "DiceId", "ProfileId", "Result" },
                values: new object[,]
                {
                    { new Guid("1fd5d81f-1fd8-497d-9895-e460d33d0a53"), 4, new Guid("15a4a021-bea3-45e5-a8ff-58d6a5d902cd"), 3 },
                    { new Guid("4a4a5bfb-7e06-4e6b-b252-a8733593b612"), 4, new Guid("15a4a021-bea3-45e5-a8ff-58d6a5d902cd"), 4 },
                    { new Guid("668c5989-0569-4239-a1ce-319d77264d7e"), 3, new Guid("5a3888f4-8bcf-4002-a86a-461515a4dd89"), 3 },
                    { new Guid("8e64d355-3951-44d3-b84b-a772df84c8a0"), 6, new Guid("5a3888f4-8bcf-4002-a86a-461515a4dd89"), 5 },
                    { new Guid("aa6f9111-b174-4064-814b-ce7eb4169e80"), 2, new Guid("cc6f9111-b174-4064-814b-ce7eb4169e80"), 1 },
                    { new Guid("dd6f9111-b174-4064-814b-ce7eb4169e80"), 2, new Guid("cc6f9111-b174-4064-814b-ce7eb4169e80"), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_throws_ProfileId",
                table: "throws",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dices");

            migrationBuilder.DropTable(
                name: "throws");

            migrationBuilder.DropTable(
                name: "profiles");
        }
    }
}
