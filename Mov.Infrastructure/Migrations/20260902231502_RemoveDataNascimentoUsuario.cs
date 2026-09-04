using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mov.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDataNascimentoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "Usuarios",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "Usuarios");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Usuarios",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
