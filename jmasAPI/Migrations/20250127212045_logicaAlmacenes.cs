using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class logicaAlmacenes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_Almacen",
                table: "Salidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Almacenes",
                columns: table => new
                {
                    Id_Almacen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    almacen_Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Almacenes", x => x.Id_Almacen);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_Id_Almacen",
                table: "Salidas",
                column: "Id_Almacen");

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Almacenes_Id_Almacen",
                table: "Salidas",
                column: "Id_Almacen",
                principalTable: "Almacenes",
                principalColumn: "Id_Almacen",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Almacenes_Id_Almacen",
                table: "Salidas");

            migrationBuilder.DropTable(
                name: "Almacenes");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_Id_Almacen",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "Id_Almacen",
                table: "Salidas");
        }
    }
}
