using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class entradas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entradas",
                columns: table => new
                {
                    Id_Entradas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Entrada_Folio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Entrada_Unidades = table.Column<double>(type: "double", nullable: false),
                    Entrada_Costo = table.Column<double>(type: "double", nullable: false),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Id_Proveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas", x => x.Id_Entradas);
                    table.ForeignKey(
                        name: "FK_Entradas_Productos_Id_Producto",
                        column: x => x.Id_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id_Producto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entradas_Proveedores_Id_Proveedor",
                        column: x => x.Id_Proveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id_Proveedor",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Id_Producto",
                table: "Entradas",
                column: "Id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Id_Proveedor",
                table: "Entradas",
                column: "Id_Proveedor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entradas");
        }
    }
}
