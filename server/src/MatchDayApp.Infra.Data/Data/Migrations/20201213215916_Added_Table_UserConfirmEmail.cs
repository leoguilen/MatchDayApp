using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MatchDayApp.Infra.Data.Data.Migrations
{
    public partial class Added_Table_UserConfirmEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserConfirmEmails",
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
                    table.PrimaryKey("PK_UserConfirmEmails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConfirmEmails");
        }
    }
}
