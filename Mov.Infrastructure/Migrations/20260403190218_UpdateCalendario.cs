using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mov.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCalendario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EscolaId",
                table: "Calendarios",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Calendarios_EscolaId",
                table: "Calendarios",
                column: "EscolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendarios_Escola_EscolaId",
                table: "Calendarios",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendarios_Escola_EscolaId",
                table: "Calendarios");

            migrationBuilder.DropIndex(
                name: "IX_Calendarios_EscolaId",
                table: "Calendarios");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "Calendarios");
        }
    }
}
