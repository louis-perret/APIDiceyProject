using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.EF.Migrations
{
    /// <inheritdoc />
    public partial class NTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "profiles",
                keyColumn: "Id",
                keyValue: new Guid("98b76d55-e413-4ad0-97ab-bed373a13a65"));

            migrationBuilder.DeleteData(
                table: "profiles",
                keyColumn: "Id",
                keyValue: new Guid("b5dab26f-6975-483c-8bb6-1cd9b19190a0"));

            migrationBuilder.DeleteData(
                table: "profiles",
                keyColumn: "Id",
                keyValue: new Guid("bd68c485-c883-44c5-a235-9190d3049d4a"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("003a02ce-db1e-405b-88a9-a6807f591c14"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("0a7aa0d4-efac-467e-b151-e6dc17facbd0"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("0a8e177e-fbd2-4ba5-80d3-506f22709592"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("1c786418-3d11-4947-8901-53b35ac76502"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("825e122e-897f-4a59-93be-c8c554a86b53"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("ed12df19-cf27-4e14-9f7c-bb3d2774a52d"));

            migrationBuilder.InsertData(
                table: "profiles",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("6af0ae0f-fb3a-4604-810a-9517d8f6a741"), "Malvezin", "Neitah" },
                    { new Guid("76a6268b-cc5f-4971-90d9-079f3720c0b9"), "Perret", "Louis" },
                    { new Guid("dcce4943-25ea-4929-a23a-427fd0a62cb7"), "Grienenberger", "Côme" }
                });

            migrationBuilder.InsertData(
                table: "throws",
                columns: new[] { "Id", "DiceId", "Result" },
                values: new object[,]
                {
                    { new Guid("1c85e2ad-608d-4596-9885-43639928342f"), 2, 1 },
                    { new Guid("3274b913-13e7-482a-a264-f061f95bbfef"), 4, 3 },
                    { new Guid("358e1804-e7e5-47db-8567-538bc48073b5"), 6, 5 },
                    { new Guid("660ed218-93a5-4bcf-a66c-8884a40f8633"), 4, 4 },
                    { new Guid("81486c85-a254-41ef-8c7f-25b9769bb89e"), 2, 2 },
                    { new Guid("a4e046a5-1bd1-4ab2-87dd-e6b77f8f5f3c"), 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "profiles",
                keyColumn: "Id",
                keyValue: new Guid("6af0ae0f-fb3a-4604-810a-9517d8f6a741"));

            migrationBuilder.DeleteData(
                table: "profiles",
                keyColumn: "Id",
                keyValue: new Guid("76a6268b-cc5f-4971-90d9-079f3720c0b9"));

            migrationBuilder.DeleteData(
                table: "profiles",
                keyColumn: "Id",
                keyValue: new Guid("dcce4943-25ea-4929-a23a-427fd0a62cb7"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("1c85e2ad-608d-4596-9885-43639928342f"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("3274b913-13e7-482a-a264-f061f95bbfef"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("358e1804-e7e5-47db-8567-538bc48073b5"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("660ed218-93a5-4bcf-a66c-8884a40f8633"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("81486c85-a254-41ef-8c7f-25b9769bb89e"));

            migrationBuilder.DeleteData(
                table: "throws",
                keyColumn: "Id",
                keyValue: new Guid("a4e046a5-1bd1-4ab2-87dd-e6b77f8f5f3c"));

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
    }
}
