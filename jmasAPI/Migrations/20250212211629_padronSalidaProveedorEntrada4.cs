using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class padronSalidaProveedorEntrada4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idPadron",
                table: "Salidas",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Id_Proveedor",
                table: "Entradas",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_idPadron",
                table: "Salidas",
                column: "idPadron");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Id_Proveedor",
                table: "Entradas",
                column: "Id_Proveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Proveedores_Id_Proveedor",
                table: "Entradas",
                column: "Id_Proveedor",
                principalTable: "Proveedores",
                principalColumn: "Id_Proveedor",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Padron_idPadron",
                table: "Salidas",
                column: "idPadron",
                principalTable: "Padron",
                principalColumn: "idPadron",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Proveedores_Id_Proveedor",
                table: "Entradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Padron_idPadron",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_idPadron",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Id_Proveedor",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "idPadron",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "Id_Proveedor",
                table: "Entradas");
        }
    }
}
