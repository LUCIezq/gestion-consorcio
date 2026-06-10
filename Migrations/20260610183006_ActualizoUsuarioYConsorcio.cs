using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticaParcial.Migrations
{
    /// <inheritdoc />
    public partial class ActualizoUsuarioYConsorcio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Consorcios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Consorcios_UserId",
                table: "Consorcios",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consorcios_Users_UserId",
                table: "Consorcios",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consorcios_Users_UserId",
                table: "Consorcios");

            migrationBuilder.DropIndex(
                name: "IX_Consorcios_UserId",
                table: "Consorcios");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Consorcios");
        }
    }
}
