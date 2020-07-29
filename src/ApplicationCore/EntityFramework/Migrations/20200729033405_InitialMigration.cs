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
                name: "Device",
                columns: table => new
                {
                    ObjId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.ObjId);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyContadores",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceObjId = table.Column<int>(nullable: true),
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
                    DeviceObjId = table.Column<int>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<long>(nullable: true),
                    DeviceObjId = table.Column<int>(nullable: true),
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
                        name: "FK_Contracts_Device_DeviceObjId",
                        column: x => x.DeviceObjId,
                        principalTable: "Device",
                        principalColumn: "ObjId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    OwnerId = table.Column<long>(nullable: true),
                    ContractId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Contracts_DeviceObjId",
                table: "Contracts",
                column: "DeviceObjId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OwnerId",
                table: "Contracts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyContadores_DeviceObjId",
                table: "DailyContadores",
                column: "DeviceObjId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyToners_DeviceObjId",
                table: "DailyToners",
                column: "DeviceObjId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ContractId",
                table: "Employee",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_OwnerId",
                table: "Employee",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ContractId",
                table: "User",
                column: "ContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyContadores");

            migrationBuilder.DropTable(
                name: "DailyToners");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Owner");
        }
    }
}
