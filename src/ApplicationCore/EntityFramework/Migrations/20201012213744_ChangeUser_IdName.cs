using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class ChangeUser_IdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractUsers_Users_UserId",
                table: "ContractUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractUsers",
                table: "ContractUsers");

            migrationBuilder.DropIndex(
                name: "IX_ContractUsers_UserId",
                table: "ContractUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ContractUsers");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "ContractUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "User_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractUsers",
                table: "ContractUsers",
                columns: new[] { "ContractId", "User_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_ContractUsers_User_Id",
                table: "ContractUsers",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractUsers_Users_User_Id",
                table: "ContractUsers",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractUsers_Users_User_Id",
                table: "ContractUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractUsers",
                table: "ContractUsers");

            migrationBuilder.DropIndex(
                name: "IX_ContractUsers_User_Id",
                table: "ContractUsers");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "ContractUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ContractUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractUsers",
                table: "ContractUsers",
                columns: new[] { "ContractId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ContractUsers_UserId",
                table: "ContractUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractUsers_Users_UserId",
                table: "ContractUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
