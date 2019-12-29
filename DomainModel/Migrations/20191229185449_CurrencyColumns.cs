using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWalletApp.DomainModel.Migrations
{
    public partial class CurrencyColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Currency",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Currency",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Currency");
        }
    }
}
