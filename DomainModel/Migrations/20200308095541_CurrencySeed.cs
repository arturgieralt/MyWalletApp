using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWalletApp.DomainModel.Migrations
{
    public partial class CurrencySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[] { 1L, "EURO", "EUR" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[] { 2L, "Polish Zloty", "PLN" });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[] { 3L, "American Dollar", "USD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
