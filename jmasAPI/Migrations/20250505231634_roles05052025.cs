using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jmasAPI.Migrations
{
    /// <inheritdoc />
    public partial class roles05052025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idRole",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    idRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    roleNombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    roleDescr = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    canView = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    canEdit = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    canDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    canManageUsers = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    canManageRoles = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.idRole);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_idRole",
                table: "Users",
                column: "idRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_idRole",
                table: "Users",
                column: "idRole",
                principalTable: "Role",
                principalColumn: "idRole",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_idRole",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Users_idRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "idRole",
                table: "Users");
        }
    }
}
