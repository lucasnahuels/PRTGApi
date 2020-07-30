using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCore.EntityFramework.Migrations
{
    public partial class UpdatedEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Owner_OwnerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Contracts_ContractId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Owner_OwnerId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owner",
                table: "Owner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Owner",
                newName: "Owners");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_OwnerId",
                table: "Employees",
                newName: "IX_Employees_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_ContractId",
                table: "Employees",
                newName: "IX_Employees_ContractId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owners",
                table: "Owners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Owners_OwnerId",
                table: "Contracts",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Contracts_ContractId",
                table: "Employees",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Owners_OwnerId",
                table: "Employees",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Owners_OwnerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Contracts_ContractId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Owners_OwnerId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owners",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Owners",
                newName: "Owner");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_OwnerId",
                table: "Employee",
                newName: "IX_Employee_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_ContractId",
                table: "Employee",
                newName: "IX_Employee_ContractId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owner",
                table: "Owner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Owner_OwnerId",
                table: "Contracts",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Contracts_ContractId",
                table: "Employee",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Owner_OwnerId",
                table: "Employee",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
