using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class Iniciar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    TecnicoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombres = table.Column<string>(type: "TEXT", nullable: false),
                    SueldoHora = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.TecnicoId);
                });

            migrationBuilder.CreateTable(
                name: "TiposTecnicos",
                columns: table => new
                {
                    TipoTecnicoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TecnicoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposTecnicos", x => x.TipoTecnicoId);
                });

            migrationBuilder.InsertData(
                table: "TiposTecnicos",
                columns: new[] { "TipoTecnicoId", "Descripcion", "TecnicoId" },
                values: new object[,]
                {
                    { 1, "Redes", 0 },
                    { 2, "Reparacion", 0 },
                    { 3, "Impresoras", 0 },
                    { 4, "Soporte", 0 },
                    { 5, "Celulares", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "TiposTecnicos");
        }
    }
}
