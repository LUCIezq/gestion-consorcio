using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticaParcial.Migrations
{
    /// <inheritdoc />
    public partial class Notificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaDeCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaDeEnvio = table.Column<DateOnly>(type: "date", nullable: false),
                    consorcioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Consorcios_consorcioId",
                        column: x => x.consorcioId,
                        principalTable: "Consorcios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_consorcioId",
                table: "Notificaciones",
                column: "consorcioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificaciones");
        }
    }
}
