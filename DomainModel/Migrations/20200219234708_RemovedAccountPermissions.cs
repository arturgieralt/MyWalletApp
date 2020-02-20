using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWalletApp.DomainModel.Migrations
{
    public partial class RemovedAccountPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountUser_AccountPermission_AccountPermissionId",
                table: "AccountUser");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountUserInvite_AccountPermission_AccountPermissionId",
                table: "AccountUserInvite");

            migrationBuilder.DropTable(
                name: "AccountPermission");

            migrationBuilder.DropIndex(
                name: "IX_AccountUserInvite_AccountPermissionId",
                table: "AccountUserInvite");

            migrationBuilder.DropIndex(
                name: "IX_AccountUser_AccountPermissionId",
                table: "AccountUser");

            migrationBuilder.DropColumn(
                name: "AccountPermissionId",
                table: "AccountUserInvite");

            migrationBuilder.DropColumn(
                name: "AccountPermissionId",
                table: "AccountUser");

            migrationBuilder.AddColumn<bool>(
                name: "AccountDelete",
                table: "AccountUserInvite",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AccountWrite",
                table: "AccountUserInvite",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccessRevoked",
                table: "AccountUserInvite",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TransactionRead",
                table: "AccountUserInvite",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TransactionWrite",
                table: "AccountUserInvite",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AccountDelete",
                table: "AccountUser",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AccountWrite",
                table: "AccountUser",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TransactionRead",
                table: "AccountUser",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TransactionWrite",
                table: "AccountUser",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountDelete",
                table: "AccountUserInvite");

            migrationBuilder.DropColumn(
                name: "AccountWrite",
                table: "AccountUserInvite");

            migrationBuilder.DropColumn(
                name: "IsAccessRevoked",
                table: "AccountUserInvite");

            migrationBuilder.DropColumn(
                name: "TransactionRead",
                table: "AccountUserInvite");

            migrationBuilder.DropColumn(
                name: "TransactionWrite",
                table: "AccountUserInvite");

            migrationBuilder.DropColumn(
                name: "AccountDelete",
                table: "AccountUser");

            migrationBuilder.DropColumn(
                name: "AccountWrite",
                table: "AccountUser");

            migrationBuilder.DropColumn(
                name: "TransactionRead",
                table: "AccountUser");

            migrationBuilder.DropColumn(
                name: "TransactionWrite",
                table: "AccountUser");

            migrationBuilder.AddColumn<long>(
                name: "AccountPermissionId",
                table: "AccountUserInvite",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AccountPermissionId",
                table: "AccountUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AccountPermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedById = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Read = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Write = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountPermission_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountPermission_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserInvite_AccountPermissionId",
                table: "AccountUserInvite",
                column: "AccountPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUser_AccountPermissionId",
                table: "AccountUser",
                column: "AccountPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPermission_CreatedById",
                table: "AccountPermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPermission_LastModifiedById",
                table: "AccountPermission",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUser_AccountPermission_AccountPermissionId",
                table: "AccountUser",
                column: "AccountPermissionId",
                principalTable: "AccountPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUserInvite_AccountPermission_AccountPermissionId",
                table: "AccountUserInvite",
                column: "AccountPermissionId",
                principalTable: "AccountPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
