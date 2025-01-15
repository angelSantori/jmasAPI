using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class salidaByFolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_Producto",
                table: "AjustesLess",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AjustesLess_Id_Producto",
                table: "AjustesLess",
                column: "Id_Producto");

            migrationBuilder.AddForeignKey(
                name: "FK_AjustesLess_Productos_Id_Producto",
                table: "AjustesLess",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "Id_Producto",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AjustesLess_Productos_Id_Producto",
                table: "AjustesLess");

            migrationBuilder.DropIndex(
                name: "IX_AjustesLess_Id_Producto",
                table: "AjustesLess");

            migrationBuilder.DropColumn(
                name: "Id_Producto",
                table: "AjustesLess");
        }
    }
}
