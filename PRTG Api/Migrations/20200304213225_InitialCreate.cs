using Microsoft.EntityFrameworkCore.Migrations;

namespace PRTG_Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ContractId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfCompañy = table.Column<string>(nullable: true),
                    Printer = table.Column<string>(nullable: true),
                    BlackAndWhiteSheets = table.Column<int>(nullable: false),
                    ColorSheets = table.Column<int>(nullable: false),
                    BlackToner = table.Column<int>(nullable: false),
                    CyanToner = table.Column<int>(nullable: false),
                    YellowToner = table.Column<int>(nullable: false),
                    MagentaToner = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ContractId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");
        }
    }
}
