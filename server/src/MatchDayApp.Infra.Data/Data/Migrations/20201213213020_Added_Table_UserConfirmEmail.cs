using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MatchDayApp.Infra.Data.Data.Migrations
{
    public partial class Added_Table_UserConfirmEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfirmEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ConfirmKey = table.Column<Guid>(nullable: false),
                    Confirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfirmEmails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmEmails_UserId",
                table: "ConfirmEmails",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmEmails");
        }
    }
}
