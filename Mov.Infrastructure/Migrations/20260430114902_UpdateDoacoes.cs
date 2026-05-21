using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Mov.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QldTampinha",
                table: "Doacoes",
                newName: "QtdTampinha");

            migrationBuilder.RenameColumn(
                name: "QldLacre",
                table: "Doacoes",
                newName: "QtdLacre");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Doacoes",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QtdTampinha",
                table: "Doacoes",
                newName: "QldTampinha");

            migrationBuilder.RenameColumn(
                name: "QtdLacre",
                table: "Doacoes",
                newName: "QldLacre");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Doacoes",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);
        }
    }
}
