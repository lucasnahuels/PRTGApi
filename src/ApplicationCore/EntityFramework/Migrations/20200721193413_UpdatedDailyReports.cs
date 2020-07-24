using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class UpdatedDailyReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeeklyPrinters",
                table: "WeeklyPrinters");

            migrationBuilder.RenameTable(
                name: "WeeklyPrinters",
                newName: "DailyPrinters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyPrinters",
                table: "DailyPrinters",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyPrinters",
                table: "DailyPrinters");

            migrationBuilder.RenameTable(
                name: "DailyPrinters",
                newName: "WeeklyPrinters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeeklyPrinters",
                table: "WeeklyPrinters",
                column: "Id");
        }
    }
}
