using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AjustesMas_Productos_Id_Producto",
                table: "AjustesMas");

            migrationBuilder.DropForeignKey(
                name: "FK_AjustesMenos_Productos_Id_Producto",
                table: "AjustesMenos");

            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Productos_Id_Producto",
                table: "Entradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Productos_Id_Producto",
                table: "Salidas");

            migrationBuilder.RenameColumn(
                name: "Id_Producto",
                table: "Salidas",
                newName: "idProducto");

            migrationBuilder.RenameIndex(
                name: "IX_Salidas_Id_Producto",
                table: "Salidas",
                newName: "IX_Salidas_idProducto");

            migrationBuilder.RenameColumn(
                name: "Id_Producto",
                table: "Entradas",
                newName: "idProducto");

            migrationBuilder.RenameIndex(
                name: "IX_Entradas_Id_Producto",
                table: "Entradas",
                newName: "IX_Entradas_idProducto");

            migrationBuilder.RenameColumn(
                name: "Id_Producto",
                table: "AjustesMenos",
                newName: "idProducto");

            migrationBuilder.RenameIndex(
                name: "IX_AjustesMenos_Id_Producto",
                table: "AjustesMenos",
                newName: "IX_AjustesMenos_idProducto");

            migrationBuilder.RenameColumn(
                name: "Id_Producto",
                table: "AjustesMas",
                newName: "idProducto");

            migrationBuilder.RenameIndex(
                name: "IX_AjustesMas_Id_Producto",
                table: "AjustesMas",
                newName: "IX_AjustesMas_idProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_AjustesMas_Productos_idProducto",
                table: "AjustesMas",
                column: "idProducto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AjustesMenos_Productos_idProducto",
                table: "AjustesMenos",
                column: "idProducto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Productos_idProducto",
                table: "Entradas",
                column: "idProducto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Productos_idProducto",
                table: "Salidas",
                column: "idProducto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AjustesMas_Productos_idProducto",
                table: "AjustesMas");

            migrationBuilder.DropForeignKey(
                name: "FK_AjustesMenos_Productos_idProducto",
                table: "AjustesMenos");

            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Productos_idProducto",
                table: "Entradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Productos_idProducto",
                table: "Salidas");

            migrationBuilder.RenameColumn(
                name: "idProducto",
                table: "Salidas",
                newName: "Id_Producto");

            migrationBuilder.RenameIndex(
                name: "IX_Salidas_idProducto",
                table: "Salidas",
                newName: "IX_Salidas_Id_Producto");

            migrationBuilder.RenameColumn(
                name: "idProducto",
                table: "Entradas",
                newName: "Id_Producto");

            migrationBuilder.RenameIndex(
                name: "IX_Entradas_idProducto",
                table: "Entradas",
                newName: "IX_Entradas_Id_Producto");

            migrationBuilder.RenameColumn(
                name: "idProducto",
                table: "AjustesMenos",
                newName: "Id_Producto");

            migrationBuilder.RenameIndex(
                name: "IX_AjustesMenos_idProducto",
                table: "AjustesMenos",
                newName: "IX_AjustesMenos_Id_Producto");

            migrationBuilder.RenameColumn(
                name: "idProducto",
                table: "AjustesMas",
                newName: "Id_Producto");

            migrationBuilder.RenameIndex(
                name: "IX_AjustesMas_idProducto",
                table: "AjustesMas",
                newName: "IX_AjustesMas_Id_Producto");

            migrationBuilder.AddForeignKey(
                name: "FK_AjustesMas_Productos_Id_Producto",
                table: "AjustesMas",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AjustesMenos_Productos_Id_Producto",
                table: "AjustesMenos",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Productos_Id_Producto",
                table: "Entradas",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Productos_Id_Producto",
                table: "Salidas",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
