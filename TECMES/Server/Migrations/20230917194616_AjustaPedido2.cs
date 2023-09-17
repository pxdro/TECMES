using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TECMES.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjustaPedido2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Producoes_OrdemProducaoId",
                table: "Producoes");

            migrationBuilder.CreateIndex(
                name: "IX_Producoes_OrdemProducaoId",
                table: "Producoes",
                column: "OrdemProducaoId",
                unique: true,
                filter: "[OrdemProducaoId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Producoes_OrdemProducaoId",
                table: "Producoes");

            migrationBuilder.CreateIndex(
                name: "IX_Producoes_OrdemProducaoId",
                table: "Producoes",
                column: "OrdemProducaoId");
        }
    }
}
