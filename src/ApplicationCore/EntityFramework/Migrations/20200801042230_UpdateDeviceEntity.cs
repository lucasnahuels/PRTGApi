using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class UpdateDeviceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Device_DeviceObjId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyContadores_Device_DeviceObjId",
                table: "DailyContadores");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyToners_Device_DeviceObjId",
                table: "DailyToners");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropIndex(
                name: "IX_DailyToners_DeviceObjId",
                table: "DailyToners");

            migrationBuilder.DropIndex(
                name: "IX_DailyContadores_DeviceObjId",
                table: "DailyContadores");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_DeviceObjId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DeviceObjId",
                table: "DailyToners");

            migrationBuilder.DropColumn(
                name: "DeviceObjId",
                table: "DailyContadores");

            migrationBuilder.DropColumn(
                name: "DeviceObjId",
                table: "Contracts");

            migrationBuilder.AddColumn<long>(
                name: "DeviceId",
                table: "DailyToners",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeviceId",
                table: "DailyContadores",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeviceId",
                table: "Contracts",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DailyToners");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DailyContadores");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "DeviceObjId",
                table: "DailyToners",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeviceObjId",
                table: "DailyContadores",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeviceObjId",
                table: "Contracts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    ObjId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.ObjId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyToners_DeviceObjId",
                table: "DailyToners",
                column: "DeviceObjId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyContadores_DeviceObjId",
                table: "DailyContadores",
                column: "DeviceObjId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DeviceObjId",
                table: "Contracts",
                column: "DeviceObjId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Device_DeviceObjId",
                table: "Contracts",
                column: "DeviceObjId",
                principalTable: "Device",
                principalColumn: "ObjId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyContadores_Device_DeviceObjId",
                table: "DailyContadores",
                column: "DeviceObjId",
                principalTable: "Device",
                principalColumn: "ObjId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyToners_Device_DeviceObjId",
                table: "DailyToners",
                column: "DeviceObjId",
                principalTable: "Device",
                principalColumn: "ObjId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
