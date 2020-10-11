using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class ChangesInTheTonersUsedTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Black",
                table: "TonersUsed");

            migrationBuilder.DropColumn(
                name: "Cyan",
                table: "TonersUsed");

            migrationBuilder.DropColumn(
                name: "Magenta",
                table: "TonersUsed");

            migrationBuilder.DropColumn(
                name: "Yellow",
                table: "TonersUsed");

            migrationBuilder.AddColumn<int>(
                name: "BlackTonersUsed",
                table: "TonersUsed",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CyanTonersUsed",
                table: "TonersUsed",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MagentaTonersUsed",
                table: "TonersUsed",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YellowTonersUsed",
                table: "TonersUsed",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlackTonersUsed",
                table: "TonersUsed");

            migrationBuilder.DropColumn(
                name: "CyanTonersUsed",
                table: "TonersUsed");

            migrationBuilder.DropColumn(
                name: "MagentaTonersUsed",
                table: "TonersUsed");

            migrationBuilder.DropColumn(
                name: "YellowTonersUsed",
                table: "TonersUsed");

            migrationBuilder.AddColumn<int>(
                name: "Black",
                table: "TonersUsed",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cyan",
                table: "TonersUsed",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Magenta",
                table: "TonersUsed",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Yellow",
                table: "TonersUsed",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
