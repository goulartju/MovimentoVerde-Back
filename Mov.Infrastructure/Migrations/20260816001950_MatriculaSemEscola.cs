using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mov.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MatriculaSemEscola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Escolas_EscolaId",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Matriculas");

            migrationBuilder.AlterColumn<Guid>(
                name: "EscolaId",
                table: "Matriculas",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Matriculas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Escolas_EscolaId",
                table: "Matriculas",
                column: "EscolaId",
                principalTable: "Escolas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matriculas_Escolas_EscolaId",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Matriculas");

            migrationBuilder.AlterColumn<Guid>(
                name: "EscolaId",
                table: "Matriculas",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Matriculas",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Escolas_EscolaId",
                table: "Matriculas",
                column: "EscolaId",
                principalTable: "Escolas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
