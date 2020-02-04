using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWalletApp.DomainModel.Migrations
{
    public partial class LatLngTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Transaction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Transaction");
        }
    }
}
