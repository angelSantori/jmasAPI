using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class local10042025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "catego",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "clades",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "diahora",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "iva",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "maxdes",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "minfac",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "pagmes",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "pordes",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "recmes",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "regele",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "salant",
                table: "LectEnviar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "catego",
                table: "LectEnviar",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "clades",
                table: "LectEnviar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "diahora",
                table: "LectEnviar",
                type: "tinyint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iva",
                table: "LectEnviar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "maxdes",
                table: "LectEnviar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "minfac",
                table: "LectEnviar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pagmes",
                table: "LectEnviar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "pordes",
                table: "LectEnviar",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "recmes",
                table: "LectEnviar",
                type: "tinyint unsigned",
                maxLength: 5,
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "regele",
                table: "LectEnviar",
                type: "tinyint unsigned",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "salant",
                table: "LectEnviar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
