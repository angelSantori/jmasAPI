using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class salidaTipoTrab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salida_TipoTrabajo",
                table: "Salidas",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salida_TipoTrabajo",
                table: "Salidas");
        }
    }
}
