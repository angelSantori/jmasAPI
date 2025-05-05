using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class HerramientasYPrestamo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Herramienta",
                columns: table => new
                {
                    idHerramienta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    htaNombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    htaEstado = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Herramienta", x => x.idHerramienta);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "htaPrestamo",
                columns: table => new
                {
                    idHtaPrestamo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    prestFechaPrest = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prestFechaDevol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idHerramienta = table.Column<int>(type: "int", nullable: false),
                    Id_User = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_htaPrestamo", x => x.idHtaPrestamo);
                    table.ForeignKey(
                        name: "FK_htaPrestamo_Herramienta_idHerramienta",
                        column: x => x.idHerramienta,
                        principalTable: "Herramienta",
                        principalColumn: "idHerramienta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_htaPrestamo_Users_Id_User",
                        column: x => x.Id_User,
                        principalTable: "Users",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_htaPrestamo_Id_User",
                table: "htaPrestamo",
                column: "Id_User");

            migrationBuilder.CreateIndex(
                name: "IX_htaPrestamo_idHerramienta",
                table: "htaPrestamo",
                column: "idHerramienta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "htaPrestamo");

            migrationBuilder.DropTable(
                name: "Herramienta");
        }
    }
}
