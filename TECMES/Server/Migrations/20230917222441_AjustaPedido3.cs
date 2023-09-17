using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TECMES.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjustaPedido3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Producoes_ProducaoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ProducaoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ProducaoId",
                table: "Pedidos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProducaoId",
                table: "Pedidos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ProducaoId",
                table: "Pedidos",
                column: "ProducaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Producoes_ProducaoId",
                table: "Pedidos",
                column: "ProducaoId",
                principalTable: "Producoes",
                principalColumn: "Id");
        }
    }
}
