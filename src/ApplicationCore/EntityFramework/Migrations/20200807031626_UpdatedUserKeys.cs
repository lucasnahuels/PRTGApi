using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class UpdatedUserKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractDevices_Device_ObjId",
                table: "ContractDevices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Device",
                table: "Device");

            migrationBuilder.RenameTable(
                name: "Device",
                newName: "Devices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "ObjId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractDevices_Devices_ObjId",
                table: "ContractDevices",
                column: "ObjId",
                principalTable: "Devices",
                principalColumn: "ObjId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractDevices_Devices_ObjId",
                table: "ContractDevices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "Device");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Device",
                table: "Device",
                column: "ObjId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractDevices_Device_ObjId",
                table: "ContractDevices",
                column: "ObjId",
                principalTable: "Device",
                principalColumn: "ObjId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
