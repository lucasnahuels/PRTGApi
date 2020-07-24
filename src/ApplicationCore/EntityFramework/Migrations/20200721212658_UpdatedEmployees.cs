using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class UpdatedEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrinterId",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "PrinterObjId",
                table: "Contracts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Printer",
                columns: table => new
                {
                    ObjId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printer", x => x.ObjId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PrinterObjId",
                table: "Contracts",
                column: "PrinterObjId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Printer_PrinterObjId",
                table: "Contracts",
                column: "PrinterObjId",
                principalTable: "Printer",
                principalColumn: "ObjId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Printer_PrinterObjId",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "Printer");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_PrinterObjId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "PrinterObjId",
                table: "Contracts");

            migrationBuilder.AddColumn<long>(
                name: "PrinterId",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
