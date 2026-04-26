using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Mov.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Alunos",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlunoId",
                table: "Matriculas",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
              name: "MatriculaId",
              table: "Doacoes",
              type: "char(36)",
              nullable: false,
              oldClrType: typeof(int),
              oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Matriculas",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

      
            migrationBuilder.AddForeignKey(
                name: "FK_Matriculas_Alunos_AlunoId",
                table: "Matriculas",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doacoes_Matriculas_MatriculaId",
                table: "Doacoes",
                column: "MatriculaId",
                principalTable: "Matriculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AlunoId",
                table: "Matriculas",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<int>(
               name: "MatriculaId",
               table: "Doacoes",
               type: "int",
               nullable: false,
               oldClrType: typeof(Guid),
               oldType: "char(36)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Matriculas",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Alunos",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);
        }
    }
}
