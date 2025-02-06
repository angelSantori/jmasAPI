using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class salidaUserAsignado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_User_Asignado",
                table: "Salidas",
                type: "int",
                nullable: false,
                defaultValue: 7);

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Id_User_Asignado",
                table: "Salidas",
                column: "Id_User_Asignado");

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Users_Id_User_Asignado",
                table: "Salidas",
                column: "Id_User_Asignado",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Users_Id_User_Asignado",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_Id_User_Asignado",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "Id_User_Asignado",
                table: "Salidas");
        }
    }
}
