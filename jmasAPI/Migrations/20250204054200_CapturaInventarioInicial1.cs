using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class CapturaInventarioInicial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CapturaInvIni",
                columns: table => new
                {
                    idInvIni = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    invIniFehca = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    invIniConteo = table.Column<double>(type: "double", nullable: false),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Id_Almacen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapturaInvIni", x => x.idInvIni);
                    table.ForeignKey(
                        name: "FK_CapturaInvIni_Almacenes_Id_Almacen",
                        column: x => x.Id_Almacen,
                        principalTable: "Almacenes",
                        principalColumn: "Id_Almacen",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CapturaInvIni_Productos_Id_Producto",
                        column: x => x.Id_Producto,
                        principalTable: "Productos",
                        principalColumn: "Id_Producto",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CapturaInvIni_Id_Almacen",
                table: "CapturaInvIni",
                column: "Id_Almacen");

            migrationBuilder.CreateIndex(
                name: "IX_CapturaInvIni_Id_Producto",
                table: "CapturaInvIni",
                column: "Id_Producto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapturaInvIni");
        }
    }
}
