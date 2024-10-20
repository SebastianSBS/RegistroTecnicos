using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class Actualizacion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "trabajosDetalles",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "Costo",
                table: "trabajosDetalles",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Articulos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<decimal>(
                name: "Costo",
                table: "Articulos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 1,
                columns: new[] { "Costo", "Precio" },
                values: new object[] { 350m, 400m });

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 2,
                columns: new[] { "Costo", "Precio" },
                values: new object[] { 800m, 970m });

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 3,
                columns: new[] { "Costo", "Precio" },
                values: new object[] { 600m, 850m });

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 4,
                columns: new[] { "Costo", "Precio" },
                values: new object[] { 750m, 1200m });

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 5,
                columns: new[] { "Costo", "Precio" },
                values: new object[] { 80m, 100m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Precio",
                table: "trabajosDetalles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Costo",
                table: "trabajosDetalles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "Precio",
                table: "Articulos",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<double>(
                name: "Costo",
                table: "Articulos",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 1,
                columns: new[] { "Costo", "Precio" },
                values: new object[] { 350.0, 400.0 });

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 2,
                columns: new[] { "Costo", "Precio" },
                values: new object[] { 800.0, 970.0 });

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 3,
                columns: new[] { "Costo", "Precio" },
                values: new object[] { 600.0, 850.0 });

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 4,
                columns: new[] { "Costo", "Precio" },
                values: new object[] { 750.0, 1200.0 });

            migrationBuilder.UpdateData(
                table: "Articulos",
                keyColumn: "ArticuloId",
                keyValue: 5,
                columns: new[] { "Costo", "Precio" },
                values: new object[] { 80.0, 100.0 });
        }
    }
}
