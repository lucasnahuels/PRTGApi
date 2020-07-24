using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class UpdatedContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Week",
                table: "WeeklyPrinters");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "WeeklyPrinters");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "WeeklyPrinters",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ContractId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_ContractId",
                table: "User",
                column: "ContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "WeeklyPrinters");

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "WeeklyPrinters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "WeeklyPrinters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
