using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class Actualizacion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_trabajosDetalles_TrabajoId",
                table: "trabajosDetalles",
                column: "TrabajoId");

            migrationBuilder.AddForeignKey(
                name: "FK_trabajosDetalles_Trabajos_TrabajoId",
                table: "trabajosDetalles",
                column: "TrabajoId",
                principalTable: "Trabajos",
                principalColumn: "TrabajoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trabajosDetalles_Trabajos_TrabajoId",
                table: "trabajosDetalles");

            migrationBuilder.DropIndex(
                name: "IX_trabajosDetalles_TrabajoId",
                table: "trabajosDetalles");
        }
    }
}
