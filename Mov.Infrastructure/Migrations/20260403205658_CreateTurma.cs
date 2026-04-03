using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mov.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendarios_Escola_EscolaId",
                table: "Calendarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Escola",
                table: "Escola");

            migrationBuilder.RenameTable(
                name: "Escola",
                newName: "Escolas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Escolas",
                table: "Escolas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Turmas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    EscolaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Nome = table.Column<string>(type: "longtext", nullable: false),
                    AnoEscolar = table.Column<int>(type: "int", nullable: false),
                    Turno = table.Column<string>(type: "longtext", nullable: false),
                    CalendarioId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turmas_Calendarios_CalendarioId",
                        column: x => x.CalendarioId,
                        principalTable: "Calendarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turmas_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RepresentanteTurmas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TurmaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AtribuidoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentanteTurmas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepresentanteTurmas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepresentanteTurmas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentanteTurmas_TurmaId",
                table: "RepresentanteTurmas",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentanteTurmas_UsuarioId",
                table: "RepresentanteTurmas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CalendarioId",
                table: "Turmas",
                column: "CalendarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_EscolaId",
                table: "Turmas",
                column: "EscolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendarios_Escolas_EscolaId",
                table: "Calendarios",
                column: "EscolaId",
                principalTable: "Escolas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calendarios_Escolas_EscolaId",
                table: "Calendarios");

            migrationBuilder.DropTable(
                name: "RepresentanteTurmas");

            migrationBuilder.DropTable(
                name: "Turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Escolas",
                table: "Escolas");

            migrationBuilder.RenameTable(
                name: "Escolas",
                newName: "Escola");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Escola",
                table: "Escola",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Calendarios_Escola_EscolaId",
                table: "Calendarios",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
