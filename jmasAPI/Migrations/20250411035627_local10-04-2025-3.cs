using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class local100420253 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idProblema",
                table: "LectEnviar",
                type: "int",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateTable(
                name: "ProblemasLectura",
                columns: table => new
                {
                    idProblema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descripcionProb = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemasLectura", x => x.idProblema);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LectEnviar_idProblema",
                table: "LectEnviar",
                column: "idProblema");

            migrationBuilder.AddForeignKey(
                name: "FK_LectEnviar_ProblemasLectura_idProblema",
                table: "LectEnviar",
                column: "idProblema",
                principalTable: "ProblemasLectura",
                principalColumn: "idProblema",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LectEnviar_ProblemasLectura_idProblema",
                table: "LectEnviar");

            migrationBuilder.DropTable(
                name: "ProblemasLectura");

            migrationBuilder.DropIndex(
                name: "IX_LectEnviar_idProblema",
                table: "LectEnviar");

            migrationBuilder.DropColumn(
                name: "idProblema",
                table: "LectEnviar");
        }
    }
}
