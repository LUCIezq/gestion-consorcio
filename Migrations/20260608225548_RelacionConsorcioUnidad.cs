using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticaParcial.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RelacionConsorcioUnidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsorcioId",
                table: "Unidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_ConsorcioId",
                table: "Unidades",
                column: "ConsorcioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Unidades_Consorcios_ConsorcioId",
                table: "Unidades",
                column: "ConsorcioId",
                principalTable: "Consorcios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unidades_Consorcios_ConsorcioId",
                table: "Unidades");

            migrationBuilder.DropIndex(
                name: "IX_Unidades_ConsorcioId",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "ConsorcioId",
                table: "Unidades");
        }
    }
}
