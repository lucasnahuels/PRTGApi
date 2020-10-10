using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class AddNewTableTonnersUsed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "DailyToners");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "DailyContadores");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateToday",
                table: "DailyToners",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateToday",
                table: "DailyContadores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TonersUsed",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<long>(nullable: false),
                    DateAndTime = table.Column<DateTime>(nullable: false),
                    Black = table.Column<int>(nullable: false),
                    Cyan = table.Column<int>(nullable: false),
                    Yellow = table.Column<int>(nullable: false),
                    Magenta = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TonersUsed", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TonersUsed");

            migrationBuilder.DropColumn(
                name: "DateToday",
                table: "DailyToners");

            migrationBuilder.DropColumn(
                name: "DateToday",
                table: "DailyContadores");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "DailyToners",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "DailyContadores",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
