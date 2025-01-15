using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class relaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_Entidad",
                table: "Entradas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_Junta",
                table: "Entradas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_User",
                table: "Entradas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Id_Entidad",
                table: "Entradas",
                column: "Id_Entidad");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Id_Junta",
                table: "Entradas",
                column: "Id_Junta");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_Id_User",
                table: "Entradas",
                column: "Id_User");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Entidades_Id_Entidad",
                table: "Entradas",
                column: "Id_Entidad",
                principalTable: "Entidades",
                principalColumn: "Id_Entidad",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Juntas_Id_Junta",
                table: "Entradas",
                column: "Id_Junta",
                principalTable: "Juntas",
                principalColumn: "Id_Junta",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Users_Id_User",
                table: "Entradas",
                column: "Id_User",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Entidades_Id_Entidad",
                table: "Entradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Juntas_Id_Junta",
                table: "Entradas");

            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Users_Id_User",
                table: "Entradas");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Id_Entidad",
                table: "Entradas");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Id_Junta",
                table: "Entradas");

            migrationBuilder.DropIndex(
                name: "IX_Entradas_Id_User",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "Id_Entidad",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "Id_Junta",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "Id_User",
                table: "Entradas");
        }
    }
}
