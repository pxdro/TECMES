using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TECMES.Server.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaProducao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_OrdensProducao_OrdemProducaoId",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "OrdemProducaoId",
                table: "Pedidos",
                newName: "ProducaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_OrdemProducaoId",
                table: "Pedidos",
                newName: "IX_Pedidos_ProducaoId");

            migrationBuilder.CreateTable(
                name: "Producoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Produzidos = table.Column<int>(type: "int", nullable: false),
                    OrdemProducaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producoes_OrdensProducao_OrdemProducaoId",
                        column: x => x.OrdemProducaoId,
                        principalTable: "OrdensProducao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producoes_OrdemProducaoId",
                table: "Producoes",
                column: "OrdemProducaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Producoes_ProducaoId",
                table: "Pedidos",
                column: "ProducaoId",
                principalTable: "Producoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Producoes_ProducaoId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "Producoes");

            migrationBuilder.RenameColumn(
                name: "ProducaoId",
                table: "Pedidos",
                newName: "OrdemProducaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ProducaoId",
                table: "Pedidos",
                newName: "IX_Pedidos_OrdemProducaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_OrdensProducao_OrdemProducaoId",
                table: "Pedidos",
                column: "OrdemProducaoId",
                principalTable: "OrdensProducao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
