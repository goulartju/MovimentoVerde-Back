using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mov.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepresentanteTurmas_Turmas_TurmaId",
                table: "RepresentanteTurmas");

            migrationBuilder.DropIndex(
                name: "IX_RepresentanteTurmas_TurmaId",
                table: "RepresentanteTurmas");

            migrationBuilder.AlterColumn<int>(
                name: "Turno",
                table: "Turmas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            // criar índice único (one-to-one)
            migrationBuilder.CreateIndex(
                name: "IX_RepresentanteTurmas_TurmaId",
                table: "RepresentanteTurmas",
                column: "TurmaId",
                unique: true);

            // adicionar FK (agora com índice único)
            migrationBuilder.AddForeignKey(
                name: "FK_RepresentanteTurmas_Turmas_TurmaId",
                table: "RepresentanteTurmas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepresentanteTurmas_Turmas_TurmaId",
                table: "RepresentanteTurmas");

            migrationBuilder.DropIndex(
                name: "IX_RepresentanteTurmas_TurmaId",
                table: "RepresentanteTurmas");

            migrationBuilder.AlterColumn<string>(
                name: "Turno",
                table: "Turmas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentanteTurmas_TurmaId",
                table: "RepresentanteTurmas",
                column: "TurmaId");
        }
    }
}
