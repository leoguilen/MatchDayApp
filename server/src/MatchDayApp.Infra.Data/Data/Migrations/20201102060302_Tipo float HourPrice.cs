using Microsoft.EntityFrameworkCore.Migrations;

namespace MatchDayApp.Infra.Data.Data.Migrations
{
    public partial class TipofloatHourPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "HourPrice",
                table: "SoccerCourts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HourPrice",
                table: "SoccerCourts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
