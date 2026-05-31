using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consorcio.Entidades.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    IdUnidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombrePropietario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApellidoPropietario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailPropietario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Superficie = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.IdUnidad);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Unidades");
        }
    }
}
