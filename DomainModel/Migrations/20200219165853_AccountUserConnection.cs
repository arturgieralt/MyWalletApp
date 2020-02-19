using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWalletApp.DomainModel.Migrations
{
    public partial class AccountUserConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountPermission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedById = table.Column<string>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: false),
                    Read = table.Column<bool>(nullable: false),
                    Write = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AccountUser",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    AccountPermissionId = table.Column<long>(nullable: false),
                    IsAccessRevoked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUser", x => new { x.AccountId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AccountUser_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUser_AccountPermission_AccountPermissionId",
                        column: x => x.AccountPermissionId,
                        principalTable: "AccountPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountUserInvite",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedById = table.Column<string>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    AccountPermissionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUserInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountUserInvite_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUserInvite_AccountPermission_AccountPermissionId",
                        column: x => x.AccountPermissionId,
                        principalTable: "AccountPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUserInvite_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUserInvite_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountUserInvite_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountPermission_CreatedById",
                table: "AccountPermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPermission_LastModifiedById",
                table: "AccountPermission",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUser_AccountPermissionId",
                table: "AccountUser",
                column: "AccountPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUser_UserId",
                table: "AccountUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserInvite_AccountId",
                table: "AccountUserInvite",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserInvite_AccountPermissionId",
                table: "AccountUserInvite",
                column: "AccountPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserInvite_CreatedById",
                table: "AccountUserInvite",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserInvite_LastModifiedById",
                table: "AccountUserInvite",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountUserInvite_UserId",
                table: "AccountUserInvite",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountUser");

            migrationBuilder.DropTable(
                name: "AccountUserInvite");

            migrationBuilder.DropTable(
                name: "AccountPermission");
        }
    }
}
