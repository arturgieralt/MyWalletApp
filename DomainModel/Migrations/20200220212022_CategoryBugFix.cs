using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWalletApp.DomainModel.Migrations
{
    public partial class CategoryBugFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Category_CurrencyId",
                table: "Transaction");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Currency_CurrencyId",
                table: "Transaction",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Currency_CurrencyId",
                table: "Transaction");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Category_CurrencyId",
                table: "Transaction",
                column: "CurrencyId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
