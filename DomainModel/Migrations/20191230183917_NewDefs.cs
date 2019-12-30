using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWalletApp.DomainModel.Migrations
{
    public partial class NewDefs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrencyId",
                table: "Transaction",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CurrencyId",
                table: "Transaction",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Category_CurrencyId",
                table: "Transaction",
                column: "CurrencyId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Category_CurrencyId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CurrencyId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Transaction");
        }
    }
}
