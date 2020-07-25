using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class addDailyTonersValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Printer_PrinterObjId",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "DailyPrinters");

            migrationBuilder.DropTable(
                name: "Printer");

            migrationBuilder.AlterColumn<string>(
                name: "PrinterObjId",
                table: "Contracts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    ObjId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.ObjId);
                });

            migrationBuilder.CreateTable(
                name: "DailyContadores",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceObjId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    BlackAndWhiteCopies = table.Column<int>(nullable: false),
                    ColorCopies = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyContadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyContadores_Device_DeviceObjId",
                        column: x => x.DeviceObjId,
                        principalTable: "Device",
                        principalColumn: "ObjId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyToners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceObjId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    BlackTonersUsed = table.Column<int>(nullable: false),
                    CyanTonersUsed = table.Column<int>(nullable: false),
                    MagentaTonersUsed = table.Column<int>(nullable: false),
                    YellowTonersUsed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyToners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyToners_Device_DeviceObjId",
                        column: x => x.DeviceObjId,
                        principalTable: "Device",
                        principalColumn: "ObjId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyContadores_DeviceObjId",
                table: "DailyContadores",
                column: "DeviceObjId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyToners_DeviceObjId",
                table: "DailyToners",
                column: "DeviceObjId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Device_PrinterObjId",
                table: "Contracts",
                column: "PrinterObjId",
                principalTable: "Device",
                principalColumn: "ObjId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Device_PrinterObjId",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "DailyContadores");

            migrationBuilder.DropTable(
                name: "DailyToners");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.AlterColumn<int>(
                name: "PrinterObjId",
                table: "Contracts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DailyPrinters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlackAndWhiteCopies = table.Column<int>(type: "integer", nullable: false),
                    ColorCopies = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PrinterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPrinters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Printer",
                columns: table => new
                {
                    ObjId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printer", x => x.ObjId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Printer_PrinterObjId",
                table: "Contracts",
                column: "PrinterObjId",
                principalTable: "Printer",
                principalColumn: "ObjId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
