using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MatchDayApp.Infra.Data.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    ConfirmedEmail = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(maxLength: 15, nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoccerCourts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Image = table.Column<string>(nullable: true),
                    HourPrice = table.Column<decimal>(nullable: false),
                    Phone = table.Column<string>(maxLength: 16, nullable: false),
                    Address = table.Column<string>(maxLength: 150, nullable: false),
                    Cep = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    OwnerUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoccerCourts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoccerCourts_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Image = table.Column<string>(nullable: true),
                    TotalPlayers = table.Column<int>(nullable: false),
                    OwnerUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleMatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstTeamId = table.Column<Guid>(nullable: false),
                    FirstTeamConfirmed = table.Column<bool>(nullable: false),
                    SecondTeamId = table.Column<Guid>(nullable: false),
                    SecondTeamConfirmed = table.Column<bool>(nullable: false),
                    SoccerCourtId = table.Column<Guid>(nullable: false),
                    TotalHours = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    MatchStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleMatches_Teams_FirstTeamId",
                        column: x => x.FirstTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ScheduleMatches_Teams_SecondTeamId",
                        column: x => x.SecondTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ScheduleMatches_SoccerCourts_SoccerCourtId",
                        column: x => x.SoccerCourtId,
                        principalTable: "SoccerCourts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeams", x => new { x.Id, x.UserId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_UserTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserTeams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleMatches_FirstTeamId",
                table: "ScheduleMatches",
                column: "FirstTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleMatches_SecondTeamId",
                table: "ScheduleMatches",
                column: "SecondTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleMatches_SoccerCourtId",
                table: "ScheduleMatches",
                column: "SoccerCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_SoccerCourts_OwnerUserId",
                table: "SoccerCourts",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OwnerUserId",
                table: "Teams",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_TeamId",
                table: "UserTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeams_UserId",
                table: "UserTeams",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleMatches");

            migrationBuilder.DropTable(
                name: "UserTeams");

            migrationBuilder.DropTable(
                name: "SoccerCourts");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
