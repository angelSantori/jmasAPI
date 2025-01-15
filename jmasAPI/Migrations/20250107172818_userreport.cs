using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class userreport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_Reporte",
                table: "Salidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_Reporte",
                table: "Entradas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_User_Reporte",
                table: "Salidas",
                column: "User_Reporte");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_User_Reporte",
                table: "Entradas",
                column: "User_Reporte");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Users_User_Reporte",
                table: "Entradas",
                column: "User_Reporte",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Users_User_Reporte",
                table: "Salidas",
                column: "User_Reporte",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Users_User_Reporte",
                table: "Entradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Users_User_Reporte",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_User_Reporte",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_User_Reporte",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "User_Reporte",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "User_Reporte",
                table: "Entradas");
        }
    }
}
