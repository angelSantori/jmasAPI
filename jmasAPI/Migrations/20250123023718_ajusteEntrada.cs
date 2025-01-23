using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class ajusteEntrada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Proveedores_Id_Proveedor",
                table: "Entradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Users_User_Reporte",
                table: "Entradas");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Id_Proveedor",
                table: "Entradas");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_User_Reporte",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "Id_Proveedor",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "User_Reporte",
                table: "Entradas");

            migrationBuilder.RenameColumn(
                name: "Entrada_Folio",
                table: "Entradas",
                newName: "Entrada_Referencia");

            migrationBuilder.AddColumn<string>(
                name: "Entrada_ImgB64Factura",
                table: "Entradas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Entrada_ImgB64Factura",
                table: "Entradas");

            migrationBuilder.RenameColumn(
                name: "Entrada_Referencia",
                table: "Entradas",
                newName: "Entrada_Folio");

            migrationBuilder.AddColumn<int>(
                name: "Id_Proveedor",
                table: "Entradas",
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
                name: "IX_Entradas_Id_Proveedor",
                table: "Entradas",
                column: "Id_Proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_User_Reporte",
                table: "Entradas",
                column: "User_Reporte");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Proveedores_Id_Proveedor",
                table: "Entradas",
                column: "Id_Proveedor",
                principalTable: "Proveedores",
                principalColumn: "Id_Proveedor",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Users_User_Reporte",
                table: "Entradas",
                column: "User_Reporte",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
