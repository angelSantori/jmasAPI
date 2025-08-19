using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class ordenServicioModificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordenServicio_Calle_idCalle",
                table: "ordenServicio");

            migrationBuilder.DropForeignKey(
                name: "FK_ordenServicio_Colonia_idColonia",
                table: "ordenServicio");

            migrationBuilder.DropIndex(
                name: "IX_ordenServicio_idCalle",
                table: "ordenServicio");

            migrationBuilder.DropIndex(
                name: "IX_ordenServicio_idColonia",
                table: "ordenServicio");

            migrationBuilder.DropColumn(
                name: "idCalle",
                table: "ordenServicio");

            migrationBuilder.DropColumn(
                name: "idColonia",
                table: "ordenServicio");

            migrationBuilder.AddColumn<string>(
                name: "comentarioOS",
                table: "ordenServicio",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comentarioOS",
                table: "ordenServicio");

            migrationBuilder.AddColumn<int>(
                name: "idCalle",
                table: "ordenServicio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idColonia",
                table: "ordenServicio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ordenServicio_idCalle",
                table: "ordenServicio",
                column: "idCalle");

            migrationBuilder.CreateIndex(
                name: "IX_ordenServicio_idColonia",
                table: "ordenServicio",
                column: "idColonia");

            migrationBuilder.AddForeignKey(
                name: "FK_ordenServicio_Calle_idCalle",
                table: "ordenServicio",
                column: "idCalle",
                principalTable: "Calle",
                principalColumn: "idCalle",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ordenServicio_Colonia_idColonia",
                table: "ordenServicio",
                column: "idColonia",
                principalTable: "Colonia",
                principalColumn: "idColonia",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
