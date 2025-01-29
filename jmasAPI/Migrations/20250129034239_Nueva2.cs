using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class Nueva2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_User",
                table: "Juntas",
                type: "int",
                nullable: true,
                defaultValue: 3);

            migrationBuilder.CreateIndex(
                name: "XX_Juntas_Id_User",
                table: "Juntas",
                column: "Id_User");

            migrationBuilder.AddForeignKey(
                name: "FK_Juntas_Users_Id_User",
                table: "Juntas",
                column: "Id_User",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Juntas_Users_Id_User",
                table: "Juntas");

            migrationBuilder.DropIndex(
                name: "IX_Juntas_Id_User",
                table: "Juntas");

            migrationBuilder.DropColumn(
                name: "Id_User",
                table: "Juntas");

            migrationBuilder.DropColumn(
                name: "Junta_Telefono",
                table: "Juntas");
        }
    }
}
