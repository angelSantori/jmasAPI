using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class error1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_idRole",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_roleidRole",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_idRole",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_roleidRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "idRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "roleidRole",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idRole",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "roleidRole",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_idRole",
                table: "Users",
                column: "idRole");

            migrationBuilder.CreateIndex(
                name: "IX_Users_roleidRole",
                table: "Users",
                column: "roleidRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_idRole",
                table: "Users",
                column: "idRole",
                principalTable: "Role",
                principalColumn: "idRole",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_roleidRole",
                table: "Users",
                column: "roleidRole",
                principalTable: "Role",
                principalColumn: "idRole");
        }
    }
}
