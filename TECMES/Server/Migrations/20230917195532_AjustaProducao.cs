using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TECMES.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjustaProducao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Produzidos",
                table: "Producoes",
                newName: "Produzido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Produzido",
                table: "Producoes",
                newName: "Produzidos");
        }
    }
}
