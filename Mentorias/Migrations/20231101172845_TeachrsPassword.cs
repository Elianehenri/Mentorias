using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentorias.Migrations
{
    /// <inheritdoc />
    public partial class TeachrsPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Teachers",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Teachers",
                newName: "password");
        }
    }
}
