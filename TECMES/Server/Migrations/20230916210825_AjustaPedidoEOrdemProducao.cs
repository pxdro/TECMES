using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TECMES.Server.Migrations
{
    /// <inheritdoc />
    public partial class AjustaPedidoEOrdemProducao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdensProducao_Pedidos_PedidoId",
                table: "OrdensProducao");

            migrationBuilder.DropIndex(
                name: "IX_OrdensProducao_PedidoId",
                table: "OrdensProducao");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "OrdensProducao");

            migrationBuilder.AddColumn<Guid>(
                name: "OrdemProducaoId",
                table: "Pedidos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_OrdemProducaoId",
                table: "Pedidos",
                column: "OrdemProducaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_OrdensProducao_OrdemProducaoId",
                table: "Pedidos",
                column: "OrdemProducaoId",
                principalTable: "OrdensProducao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_OrdensProducao_OrdemProducaoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_OrdemProducaoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "OrdemProducaoId",
                table: "Pedidos");

            migrationBuilder.AddColumn<Guid>(
                name: "PedidoId",
                table: "OrdensProducao",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdensProducao_PedidoId",
                table: "OrdensProducao",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdensProducao_Pedidos_PedidoId",
                table: "OrdensProducao",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }
    }
}
