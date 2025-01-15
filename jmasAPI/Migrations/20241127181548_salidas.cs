using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class salidas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salidas",
                columns: table => new
                {
                    Id_Salida = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Salida_Folio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Salida_Costo = table.Column<double>(type: "double", nullable: false),
                    Salida_Unidades = table.Column<double>(type: "double", nullable: false),
                    Id_Producto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salidas", x => x.Id_Salida);
                    table.ForeignKey(
                        name: "FK_Salidas_Productos_Id_Producto",
                        column: x => x.Id_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id_Producto",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Id_Producto",
                table: "Salidas",
                column: "Id_Producto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Salidas");
        }
    }
}
