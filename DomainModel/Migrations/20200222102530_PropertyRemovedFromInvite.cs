using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWalletApp.DomainModel.Migrations
{
    public partial class PropertyRemovedFromInvite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccessRevoked",
                table: "AccountUserInvite");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccessRevoked",
                table: "AccountUserInvite",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
