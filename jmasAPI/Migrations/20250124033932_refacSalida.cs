using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class refacSalida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Proveedores_Id_Proveedor",
                table: "Salidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Users_User_Reporte",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_Id_Proveedor",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_User_Reporte",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "Id_Proveedor",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "User_Reporte",
                table: "Salidas");

            migrationBuilder.RenameColumn(
                name: "Salida_Folio",
                table: "Salidas",
                newName: "Salida_Referencia");

            migrationBuilder.AddColumn<string>(
                name: "Salida_CodFolio",
                table: "Salidas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salida_CodFolio",
                table: "Salidas");

            migrationBuilder.RenameColumn(
                name: "Salida_Referencia",
                table: "Salidas",
                newName: "Salida_Folio");

            migrationBuilder.AddColumn<int>(
                name: "Id_Proveedor",
                table: "Salidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_Reporte",
                table: "Salidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Id_Proveedor",
                table: "Salidas",
                column: "Id_Proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_User_Reporte",
                table: "Salidas",
                column: "User_Reporte");

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Proveedores_Id_Proveedor",
                table: "Salidas",
                column: "Id_Proveedor",
                principalTable: "Proveedores",
                principalColumn: "Id_Proveedor",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Users_User_Reporte",
                table: "Salidas",
                column: "User_Reporte",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
