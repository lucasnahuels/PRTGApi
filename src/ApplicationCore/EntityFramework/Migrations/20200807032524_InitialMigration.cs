using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyContadores",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    BlackAndWhiteCopies = table.Column<int>(nullable: false),
                    ColorCopies = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyContadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyToners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    BlackTonersUsed = table.Column<int>(nullable: false),
                    CyanTonersUsed = table.Column<int>(nullable: false),
                    MagentaTonersUsed = table.Column<int>(nullable: false),
                    YellowTonersUsed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyToners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    ObjId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.ObjId);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<long>(nullable: false),
                    BlackAndWhiteLimitSet = table.Column<int>(nullable: false),
                    ColorLimitSet = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    BlackAndWhitePrice = table.Column<float>(nullable: false),
                    ColorPrice = table.Column<float>(nullable: false),
                    SurplusBlackAndWhitePrice = table.Column<float>(nullable: false),
                    SurplusColorPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    OwnerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_ContractDevices_Devices_ObjId",
                        column: x => x.ObjId,
                        principalTable: "Devices",
                        principalColumn: "ObjId",
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

            migrationBuilder.CreateIndex(
                name: "IX_ContractDevices_ObjId",
                table: "ContractDevices",
                column: "ObjId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractEmployees_EmployeeId",
                table: "ContractEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OwnerId",
                table: "Contracts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractUsers_UserId",
                table: "ContractUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OwnerId",
                table: "Employees",
                column: "OwnerId");
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
                name: "DailyContadores");

            migrationBuilder.DropTable(
                name: "DailyToners");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
