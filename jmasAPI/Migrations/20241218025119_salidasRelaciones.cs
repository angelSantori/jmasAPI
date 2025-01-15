using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class salidasRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Entrada_Fecha",
                table: "Salidas",
                newName: "Salida_Fecha");

            migrationBuilder.AddColumn<int>(
                name: "Id_Entidad",
                table: "Salidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_Junta",
                table: "Salidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_User",
                table: "Salidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Id_Entidad",
                table: "Salidas",
                column: "Id_Entidad");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Id_Junta",
                table: "Salidas",
                column: "Id_Junta");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Id_User",
                table: "Salidas",
                column: "Id_User");

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Entidades_Id_Entidad",
                table: "Salidas",
                column: "Id_Entidad",
                principalTable: "Entidades",
                principalColumn: "Id_Entidad",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Juntas_Id_Junta",
                table: "Salidas",
                column: "Id_Junta",
                principalTable: "Juntas",
                principalColumn: "Id_Junta",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Users_Id_User",
                table: "Salidas",
                column: "Id_User",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Entidades_Id_Entidad",
                table: "Salidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Juntas_Id_Junta",
                table: "Salidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Users_Id_User",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_Id_Entidad",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_Id_Junta",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_Id_User",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "Id_Entidad",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "Id_Junta",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "Id_User",
                table: "Salidas");

            migrationBuilder.RenameColumn(
                name: "Salida_Fecha",
                table: "Salidas",
                newName: "Entrada_Fecha");
        }
    }
}
