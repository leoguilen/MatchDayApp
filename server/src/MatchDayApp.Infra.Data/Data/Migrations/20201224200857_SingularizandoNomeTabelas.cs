using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MatchDayApp.Infra.Data.Data.Migrations
{
    public partial class SingularizandoNomeTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Times_PrimeiroTimeId",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Quadras_QuadraFutebolId",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Times_SegundoTimeId",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Quadras_Usuarios_UsuarioProprietarioId",
                table: "Quadras");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Usuarios_UsuarioProprietarioId",
                table: "Times");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTimes_Times_TimeId",
                table: "UsuarioTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTimes_Usuarios_UsuarioId",
                table: "UsuarioTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioTimes",
                table: "UsuarioTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Times",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quadras",
                table: "Quadras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partidas",
                table: "Partidas");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("ffa9e4f2-94db-4675-aa8b-f6d2b6ec997a"));

            migrationBuilder.RenameTable(
                name: "UsuarioTimes",
                newName: "UsuarioTime");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Times",
                newName: "Time");

            migrationBuilder.RenameTable(
                name: "Quadras",
                newName: "QuadraFutebol");

            migrationBuilder.RenameTable(
                name: "Partidas",
                newName: "Partida");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioTimes_UsuarioId",
                table: "UsuarioTime",
                newName: "IX_UsuarioTime_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioTimes_TimeId",
                table: "UsuarioTime",
                newName: "IX_UsuarioTime_TimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Username",
                table: "Usuario",
                newName: "IX_Usuario_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Email",
                table: "Usuario",
                newName: "IX_Usuario_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Times_UsuarioProprietarioId",
                table: "Time",
                newName: "IX_Time_UsuarioProprietarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Quadras_UsuarioProprietarioId",
                table: "QuadraFutebol",
                newName: "IX_QuadraFutebol_UsuarioProprietarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Partidas_SegundoTimeId",
                table: "Partida",
                newName: "IX_Partida_SegundoTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Partidas_QuadraFutebolId",
                table: "Partida",
                newName: "IX_Partida_QuadraFutebolId");

            migrationBuilder.RenameIndex(
                name: "IX_Partidas_PrimeiroTimeId",
                table: "Partida",
                newName: "IX_Partida_PrimeiroTimeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioTime",
                table: "UsuarioTime",
                columns: new[] { "Id", "UsuarioId", "TimeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Time",
                table: "Time",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuadraFutebol",
                table: "QuadraFutebol",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partida",
                table: "Partida",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Avatar", "CriadoEm", "Deletado", "Email", "EmailConfirmado", "Nome", "Salt", "Senha", "Sobrenome", "Telefone", "TipoUsuario", "Username" },
                values: new object[] { new Guid("6e9fe21f-9406-42f0-a912-c2262d55b14c"), "", new DateTime(2020, 12, 24, 17, 8, 56, 904, DateTimeKind.Local).AddTicks(7447), false, "desenvolvimento.dev1@gmail.com", true, "Administrador", "ugYNNnYEnlQ=", "jH/mbPAdoAPAbKdk2OB0fOOIgGxLffK8X3ADOusg4sE=", "Master", "+55 (11)1020-3040", 3, "admin.master" });

            migrationBuilder.AddForeignKey(
                name: "FK_Partida_Time_PrimeiroTimeId",
                table: "Partida",
                column: "PrimeiroTimeId",
                principalTable: "Time",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Partida_QuadraFutebol_QuadraFutebolId",
                table: "Partida",
                column: "QuadraFutebolId",
                principalTable: "QuadraFutebol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partida_Time_SegundoTimeId",
                table: "Partida",
                column: "SegundoTimeId",
                principalTable: "Time",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_QuadraFutebol_Usuario_UsuarioProprietarioId",
                table: "QuadraFutebol",
                column: "UsuarioProprietarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Time_Usuario_UsuarioProprietarioId",
                table: "Time",
                column: "UsuarioProprietarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTime_Time_TimeId",
                table: "UsuarioTime",
                column: "TimeId",
                principalTable: "Time",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTime_Usuario_UsuarioId",
                table: "UsuarioTime",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partida_Time_PrimeiroTimeId",
                table: "Partida");

            migrationBuilder.DropForeignKey(
                name: "FK_Partida_QuadraFutebol_QuadraFutebolId",
                table: "Partida");

            migrationBuilder.DropForeignKey(
                name: "FK_Partida_Time_SegundoTimeId",
                table: "Partida");

            migrationBuilder.DropForeignKey(
                name: "FK_QuadraFutebol_Usuario_UsuarioProprietarioId",
                table: "QuadraFutebol");

            migrationBuilder.DropForeignKey(
                name: "FK_Time_Usuario_UsuarioProprietarioId",
                table: "Time");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTime_Time_TimeId",
                table: "UsuarioTime");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioTime_Usuario_UsuarioId",
                table: "UsuarioTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioTime",
                table: "UsuarioTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Time",
                table: "Time");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuadraFutebol",
                table: "QuadraFutebol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partida",
                table: "Partida");

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: new Guid("6e9fe21f-9406-42f0-a912-c2262d55b14c"));

            migrationBuilder.RenameTable(
                name: "UsuarioTime",
                newName: "UsuarioTimes");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Time",
                newName: "Times");

            migrationBuilder.RenameTable(
                name: "QuadraFutebol",
                newName: "Quadras");

            migrationBuilder.RenameTable(
                name: "Partida",
                newName: "Partidas");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioTime_UsuarioId",
                table: "UsuarioTimes",
                newName: "IX_UsuarioTimes_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioTime_TimeId",
                table: "UsuarioTimes",
                newName: "IX_UsuarioTimes_TimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_Username",
                table: "Usuarios",
                newName: "IX_Usuarios_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Usuario_Email",
                table: "Usuarios",
                newName: "IX_Usuarios_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Time_UsuarioProprietarioId",
                table: "Times",
                newName: "IX_Times_UsuarioProprietarioId");

            migrationBuilder.RenameIndex(
                name: "IX_QuadraFutebol_UsuarioProprietarioId",
                table: "Quadras",
                newName: "IX_Quadras_UsuarioProprietarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Partida_SegundoTimeId",
                table: "Partidas",
                newName: "IX_Partidas_SegundoTimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Partida_QuadraFutebolId",
                table: "Partidas",
                newName: "IX_Partidas_QuadraFutebolId");

            migrationBuilder.RenameIndex(
                name: "IX_Partida_PrimeiroTimeId",
                table: "Partidas",
                newName: "IX_Partidas_PrimeiroTimeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioTimes",
                table: "UsuarioTimes",
                columns: new[] { "Id", "UsuarioId", "TimeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Times",
                table: "Times",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quadras",
                table: "Quadras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partidas",
                table: "Partidas",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Avatar", "CriadoEm", "Deletado", "Email", "EmailConfirmado", "Nome", "Salt", "Senha", "Sobrenome", "Telefone", "TipoUsuario", "Username" },
                values: new object[] { new Guid("ffa9e4f2-94db-4675-aa8b-f6d2b6ec997a"), "", new DateTime(2020, 12, 22, 18, 3, 35, 578, DateTimeKind.Local).AddTicks(9287), false, "desenvolvimento.dev1@gmail.com", true, "Administrador", "5faZt0QPw1U=", "eDT9dORv00PU5SqHm0PH8C1fuhUPCJmkWDESHg3w/no=", "Master", "+55 (11)1020-3040", 3, "admin.master" });

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Times_PrimeiroTimeId",
                table: "Partidas",
                column: "PrimeiroTimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Quadras_QuadraFutebolId",
                table: "Partidas",
                column: "QuadraFutebolId",
                principalTable: "Quadras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Times_SegundoTimeId",
                table: "Partidas",
                column: "SegundoTimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quadras_Usuarios_UsuarioProprietarioId",
                table: "Quadras",
                column: "UsuarioProprietarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Usuarios_UsuarioProprietarioId",
                table: "Times",
                column: "UsuarioProprietarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTimes_Times_TimeId",
                table: "UsuarioTimes",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioTimes_Usuarios_UsuarioId",
                table: "UsuarioTimes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
