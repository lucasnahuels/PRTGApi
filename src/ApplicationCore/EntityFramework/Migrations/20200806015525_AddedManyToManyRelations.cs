using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class AddedManyToManyRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Contracts_ContractId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Contracts_ContractId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ContractId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ContractId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "ContractEmployees",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(nullable: false),
                    ContractId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractEmployees", x => new { x.ContractId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_ContractEmployees_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ContractId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractUsers", x => new { x.ContractId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ContractUsers_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "ContractDevices",
                columns: table => new
                {
                    ObjId = table.Column<string>(nullable: false),
                    ContractId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDevices", x => new { x.ContractId, x.ObjId });
                    table.ForeignKey(
                        name: "FK_ContractDevices_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractDevices_Device_ObjId",
                        column: x => x.ObjId,
                        principalTable: "Device",
                        principalColumn: "ObjId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractDevices_ObjId",
                table: "ContractDevices",
                column: "ObjId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractEmployees_EmployeeId",
                table: "ContractEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractUsers_UserId",
                table: "ContractUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDevices");

            migrationBuilder.DropTable(
                name: "ContractEmployees");

            migrationBuilder.DropTable(
                name: "ContractUsers");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddColumn<long>(
                name: "ContractId",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeviceId",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ContractId",
                table: "User",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ContractId",
                table: "Employees",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ContractId",
                table: "User",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Contracts_ContractId",
                table: "Employees",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Contracts_ContractId",
                table: "User",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
