using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class ajUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_User",
                table: "AjustesMenos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_User",
                table: "AjustesMas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AjustesMenos_Id_User",
                table: "AjustesMenos",
                column: "Id_User");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesMas_Id_User",
                table: "AjustesMas",
                column: "Id_User");

            migrationBuilder.AddForeignKey(
                name: "FK_AjustesMas_Users_Id_User",
                table: "AjustesMas",
                column: "Id_User",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AjustesMenos_Users_Id_User",
                table: "AjustesMenos",
                column: "Id_User",
                principalTable: "Users",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AjustesMas_Users_Id_User",
                table: "AjustesMas");

            migrationBuilder.DropForeignKey(
                name: "FK_AjustesMenos_Users_Id_User",
                table: "AjustesMenos");

            migrationBuilder.DropIndex(
                name: "IX_AjustesMenos_Id_User",
                table: "AjustesMenos");

            migrationBuilder.DropIndex(
                name: "IX_AjustesMas_Id_User",
                table: "AjustesMas");

            migrationBuilder.DropColumn(
                name: "Id_User",
                table: "AjustesMenos");

            migrationBuilder.DropColumn(
                name: "Id_User",
                table: "AjustesMas");
        }
    }
}
