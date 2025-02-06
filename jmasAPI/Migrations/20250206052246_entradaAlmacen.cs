using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class entradaAlmacen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_Almacen",
                table: "Entradas",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Id_Almacen",
                table: "Entradas",
                column: "Id_Almacen");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Almacenes_Id_Almacen",
                table: "Entradas",
                column: "Id_Almacen",
                principalTable: "Almacenes",
                principalColumn: "Id_Almacen",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Almacenes_Id_Almacen",
                table: "Entradas");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Id_Almacen",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "Id_Almacen",
                table: "Entradas");
        }
    }
}
