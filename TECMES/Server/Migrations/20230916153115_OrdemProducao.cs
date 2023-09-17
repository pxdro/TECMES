﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TECMES.Server.Migrations
{
    /// <inheritdoc />
    public partial class OrdemProducao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrdemProducao",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrdemProducao",
                table: "Usuarios");
        }
    }
}
