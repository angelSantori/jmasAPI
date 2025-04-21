using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class local170425 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "conteo",
                table: "LectEnviar",
                type: "int",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AddColumn<string>(
                name: "ubicacion",
                table: "LectEnviar",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "conteo",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "ubicacion",
                table: "LectEnviar");
        }
    }
}
