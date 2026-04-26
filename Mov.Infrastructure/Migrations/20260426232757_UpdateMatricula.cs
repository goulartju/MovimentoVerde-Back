using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mov.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMatricula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EscolaId",
                table: "Matriculas",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_EscolaId",
                table: "Matriculas",
                column: "EscolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Escolas_EscolaId",
                table: "Matriculas",
                column: "EscolaId",
                principalTable: "Escolas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Escolas_EscolaId",
                table: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_Matriculas_EscolaId",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "Matriculas");
        }
    }
}
