using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class refacAjustes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AjustesLess");

            migrationBuilder.DropTable(
                name: "AjustesMore");

            migrationBuilder.CreateTable(
                name: "AjustesMas",
                columns: table => new
                {
                    Id_AjusteMas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AjuesteMas_Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AjusteMas_Cantidad = table.Column<double>(type: "double", nullable: false),
                    AjusteMas_Fecha = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Producto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustesMas", x => x.Id_AjusteMas);
                    table.ForeignKey(
                        name: "FK_AjustesMas_Productos_Id_Producto",
                        column: x => x.Id_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id_Producto",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AjustesMenos",
                columns: table => new
                {
                    Id_AjusteMenos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AjuesteMenos_Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AjusteMenos_Cantidad = table.Column<double>(type: "double", nullable: false),
                    AjusteMenos_Fecha = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Producto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustesMenos", x => x.Id_AjusteMenos);
                    table.ForeignKey(
                        name: "FK_AjustesMenos_Productos_Id_Producto",
                        column: x => x.Id_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id_Producto",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesMas_Id_Producto",
                table: "AjustesMas",
                column: "Id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesMenos_Id_Producto",
                table: "AjustesMenos",
                column: "Id_Producto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AjustesMas");

            migrationBuilder.DropTable(
                name: "AjustesMenos");

            migrationBuilder.CreateTable(
                name: "AjustesLess",
                columns: table => new
                {
                    Id_AjusteLess = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AjusteLess_Cantidad = table.Column<double>(type: "double", nullable: false),
                    AjusteLess_Fecha = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Entrada = table.Column<int>(type: "int", nullable: true),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Id_Salida = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustesLess", x => x.Id_AjusteLess);
                    table.ForeignKey(
                        name: "FK_AjustesLess_Entradas_Id_Entrada",
                        column: x => x.Id_Entrada,
                        principalTable: "Entradas",
                        principalColumn: "Id_Entradas",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AjustesLess_Productos_Id_Producto",
                        column: x => x.Id_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id_Producto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AjustesLess_Salidas_Id_Salida",
                        column: x => x.Id_Salida,
                        principalTable: "Salidas",
                        principalColumn: "Id_Salida",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AjustesMore",
                columns: table => new
                {
                    Id_AjusteMore = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AjusteMore_Cantidad = table.Column<double>(type: "double", nullable: false),
                    AjusteMore_Fecha = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Entradas = table.Column<int>(type: "int", nullable: true),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Id_Salida = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustesMore", x => x.Id_AjusteMore);
                    table.ForeignKey(
                        name: "FK_AjustesMore_Entradas_Id_Entradas",
                        column: x => x.Id_Entradas,
                        principalTable: "Entradas",
                        principalColumn: "Id_Entradas",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AjustesMore_Productos_Id_Producto",
                        column: x => x.Id_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id_Producto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AjustesMore_Salidas_Id_Salida",
                        column: x => x.Id_Salida,
                        principalTable: "Salidas",
                        principalColumn: "Id_Salida",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesLess_Id_Entrada",
                table: "AjustesLess",
                column: "Id_Entrada");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesLess_Id_Producto",
                table: "AjustesLess",
                column: "Id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesLess_Id_Salida",
                table: "AjustesLess",
                column: "Id_Salida");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesMore_Id_Entradas",
                table: "AjustesMore",
                column: "Id_Entradas");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesMore_Id_Producto",
                table: "AjustesMore",
                column: "Id_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesMore_Id_Salida",
                table: "AjustesMore",
                column: "Id_Salida");
        }
    }
}
