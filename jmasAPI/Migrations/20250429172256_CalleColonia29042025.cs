using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class CalleColonia29042025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idCalle",
                table: "Salidas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idColonia",
                table: "Salidas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calle",
                columns: table => new
                {
                    idCalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    calleNombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calle", x => x.idCalle);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Colonia",
                columns: table => new
                {
                    idColonia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombreColonia = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colonia", x => x.idColonia);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_idCalle",
                table: "Salidas",
                column: "idCalle");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_idColonia",
                table: "Salidas",
                column: "idColonia");

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Calle_idCalle",
                table: "Salidas",
                column: "idCalle",
                principalTable: "Calle",
                principalColumn: "idCalle",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salidas_Colonia_idColonia",
                table: "Salidas",
                column: "idColonia",
                principalTable: "Colonia",
                principalColumn: "idColonia",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Calle_idCalle",
                table: "Salidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salidas_Colonia_idColonia",
                table: "Salidas");

            migrationBuilder.DropTable(
                name: "Calle");

            migrationBuilder.DropTable(
                name: "Colonia");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_idCalle",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_idColonia",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "idCalle",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "idColonia",
                table: "Salidas");
        }
    }
}
