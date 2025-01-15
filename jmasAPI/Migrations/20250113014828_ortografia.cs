using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class ortografia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AjuesteMenos_Descripcion",
                table: "AjustesMenos",
                newName: "AjusteMenos_Descripcion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AjusteMenos_Descripcion",
                table: "AjustesMenos",
                newName: "AjuesteMenos_Descripcion");
        }
    }
}
