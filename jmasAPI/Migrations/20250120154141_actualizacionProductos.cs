using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class actualizacionProductos : Migration
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productos",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Producto_ImgBase64",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Producto_Precio1",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Producto_Precio2",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Producto_UMedida",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "Producto_Precio3",
                table: "Productos",
                newName: "prodPrecio");

            migrationBuilder.RenameColumn(
                name: "Producto_ExistenciaInicial",
                table: "Productos",
                newName: "prodMin");

            migrationBuilder.RenameColumn(
                name: "Producto_ExistenciaConFis",
                table: "Productos",
                newName: "prodMax");

            migrationBuilder.RenameColumn(
                name: "Producto_Existencia",
                table: "Productos",
                newName: "prodExistencia");

            migrationBuilder.RenameColumn(
                name: "Producto_Descripcion",
                table: "Productos",
                newName: "prodDescripcion");

            migrationBuilder.RenameColumn(
                name: "Producto_Costo",
                table: "Productos",
                newName: "prodCosto");

            migrationBuilder.RenameColumn(
                name: "Id_Producto",
                table: "Productos",
                newName: "idProveedor");

            migrationBuilder.AlterColumn<int>(
                name: "idProveedor",
                table: "Productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "idProducto",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "prodImgB64",
                table: "Productos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "prodUMedEntrada",
                table: "Productos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "prodUMedSalida",
                table: "Productos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productos",
                table: "Productos",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_idProveedor",
                table: "Productos",
                column: "idProveedor");

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
                name: "FK_Productos_Proveedores_idProveedor",
                table: "Productos",
                column: "idProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id_Proveedor",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Productos_Id_Producto",
                table: "Salidas",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Productos_Proveedores_idProveedor",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Productos_Id_Producto",
                table: "Salidas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productos",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_idProveedor",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "idProducto",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "prodImgB64",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "prodUMedEntrada",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "prodUMedSalida",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "prodPrecio",
                table: "Productos",
                newName: "Producto_Precio3");

            migrationBuilder.RenameColumn(
                name: "prodMin",
                table: "Productos",
                newName: "Producto_ExistenciaInicial");

            migrationBuilder.RenameColumn(
                name: "prodMax",
                table: "Productos",
                newName: "Producto_ExistenciaConFis");

            migrationBuilder.RenameColumn(
                name: "prodExistencia",
                table: "Productos",
                newName: "Producto_Existencia");

            migrationBuilder.RenameColumn(
                name: "prodDescripcion",
                table: "Productos",
                newName: "Producto_Descripcion");

            migrationBuilder.RenameColumn(
                name: "prodCosto",
                table: "Productos",
                newName: "Producto_Costo");

            migrationBuilder.RenameColumn(
                name: "idProveedor",
                table: "Productos",
                newName: "Id_Producto");

            migrationBuilder.AlterColumn<int>(
                name: "Id_Producto",
                table: "Productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Producto_ImgBase64",
                table: "Productos",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Producto_Precio1",
                table: "Productos",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Producto_Precio2",
                table: "Productos",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Producto_UMedida",
                table: "Productos",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productos",
                table: "Productos",
                column: "Id_Producto");

            migrationBuilder.AddForeignKey(
                name: "FK_AjustesMas_Productos_Id_Producto",
                table: "AjustesMas",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "Id_Producto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AjustesMenos_Productos_Id_Producto",
                table: "AjustesMenos",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "Id_Producto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Productos_Id_Producto",
                table: "Entradas",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "Id_Producto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Productos_Id_Producto",
                table: "Salidas",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "Id_Producto",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
