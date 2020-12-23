using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MatchDayApp.Infra.Data.Data.Migrations
{
    public partial class AlterandoNomenclaturaDasTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleMatches");

            migrationBuilder.DropTable(
                name: "UserConfirmEmails");

            migrationBuilder.DropTable(
                name: "UserTeams");

            migrationBuilder.DropTable(
                name: "SoccerCourts");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "ConfirmacaoEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequisicaoEm = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    ChaveConfirmacao = table.Column<Guid>(nullable: false),
                    Confirmado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmacaoEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Sobrenome = table.Column<string>(maxLength: 50, nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    EmailConfirmado = table.Column<bool>(nullable: false),
                    Telefone = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(maxLength: 15, nullable: false),
                    TipoUsuario = table.Column<int>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quadras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Imagem = table.Column<string>(nullable: true),
                    PrecoHora = table.Column<double>(type: "float", nullable: false),
                    Telefone = table.Column<string>(maxLength: 16, nullable: false),
                    Endereco = table.Column<string>(maxLength: 150, nullable: false),
                    Cep = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    UsuarioProprietarioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quadras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quadras_Usuarios_UsuarioProprietarioId",
                        column: x => x.UsuarioProprietarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Imagem = table.Column<string>(nullable: true),
                    QtdIntegrantes = table.Column<int>(nullable: false),
                    UsuarioProprietarioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Times_Usuarios_UsuarioProprietarioId",
                        column: x => x.UsuarioProprietarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PrimeiroTimeId = table.Column<Guid>(nullable: false),
                    PrimeiroTimeConfirmado = table.Column<bool>(nullable: false),
                    SegundoTimeId = table.Column<Guid>(nullable: false),
                    SegundoTimeConfirmado = table.Column<bool>(nullable: false),
                    QuadraFutebolId = table.Column<Guid>(nullable: false),
                    HorasPartida = table.Column<int>(nullable: false),
                    DataPartida = table.Column<DateTime>(nullable: false),
                    StatusPartida = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidas_Times_PrimeiroTimeId",
                        column: x => x.PrimeiroTimeId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Partidas_Quadras_QuadraFutebolId",
                        column: x => x.QuadraFutebolId,
                        principalTable: "Quadras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partidas_Times_SegundoTimeId",
                        column: x => x.SegundoTimeId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    TimeId = table.Column<Guid>(nullable: false),
                    Aceito = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTimes", x => new { x.Id, x.UsuarioId, x.TimeId });
                    table.ForeignKey(
                        name: "FK_UsuarioTimes_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioTimes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Avatar", "CriadoEm", "Deletado", "Email", "EmailConfirmado", "Nome", "Salt", "Senha", "Sobrenome", "Telefone", "TipoUsuario", "Username" },
                values: new object[] { new Guid("ffa9e4f2-94db-4675-aa8b-f6d2b6ec997a"), "", new DateTime(2020, 12, 22, 18, 3, 35, 578, DateTimeKind.Local).AddTicks(9287), false, "desenvolvimento.dev1@gmail.com", true, "Administrador", "5faZt0QPw1U=", "eDT9dORv00PU5SqHm0PH8C1fuhUPCJmkWDESHg3w/no=", "Master", "+55 (11)1020-3040", 3, "admin.master" });

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_PrimeiroTimeId",
                table: "Partidas",
                column: "PrimeiroTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_QuadraFutebolId",
                table: "Partidas",
                column: "QuadraFutebolId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_SegundoTimeId",
                table: "Partidas",
                column: "SegundoTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Quadras_UsuarioProprietarioId",
                table: "Quadras",
                column: "UsuarioProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Times_UsuarioProprietarioId",
                table: "Times",
                column: "UsuarioProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Username",
                table: "Usuarios",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTimes_TimeId",
                table: "UsuarioTimes",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTimes_UsuarioId",
                table: "UsuarioTimes",
                column: "UsuarioId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmacaoEmails");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "UsuarioTimes");

            migrationBuilder.DropTable(
                name: "Quadras");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.CreateTable(
                name: "UserConfirmEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfirmKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfirmEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmedEmail = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoccerCourts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourPrice = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoccerCourts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoccerCourts_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPlayers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleMatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstTeamConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FirstTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchStatus = table.Column<int>(type: "int", nullable: false),
                    SecondTeamConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecondTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoccerCourtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleMatches_Teams_FirstTeamId",
                        column: x => x.FirstTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleMatches_Teams_SecondTeamId",
                        column: x => x.SecondTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleMatches_SoccerCourts_SoccerCourtId",
                        column: x => x.SoccerCourtId,
                        principalTable: "SoccerCourts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeams", x => new { x.Id, x.UserId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_UserTeams_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
    }
}
