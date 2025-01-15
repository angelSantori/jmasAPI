using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class prodAjMor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_Producto",
                table: "AjustesMore",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AjustesMore_Id_Producto",
                table: "AjustesMore",
                column: "Id_Producto");

            migrationBuilder.AddForeignKey(
                name: "FK_AjustesMore_Productos_Id_Producto",
                table: "AjustesMore",
                column: "Id_Producto",
                principalTable: "Productos",
                principalColumn: "Id_Producto",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AjustesMore_Productos_Id_Producto",
                table: "AjustesMore");

            migrationBuilder.DropIndex(
                name: "IX_AjustesMore_Id_Producto",
                table: "AjustesMore");

            migrationBuilder.DropColumn(
                name: "Id_Producto",
                table: "AjustesMore");
        }
    }
}
