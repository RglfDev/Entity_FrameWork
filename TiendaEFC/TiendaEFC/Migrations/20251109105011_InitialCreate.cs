using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TiendaEFC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    NumeroExistencias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedorId);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    CompraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.CompraId);
                    table.ForeignKey(
                        name: "FK_Compras_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compras_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suministros",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suministros", x => new { x.ProveedorId, x.ProductoId });
                    table.ForeignKey(
                        name: "FK_Suministros_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suministros_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "Apellido", "Direccion", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "López", "C/ Alcalá 10", "Ana", "600123456" },
                    { 2, "Pérez", "Av. de América 80", "Luis", "600654321" },
                    { 3, "Gómez", "C/ Serrano 42", "Marta", "601998877" },
                    { 4, "Ruiz", "C/ Toledo 14", "Carlos", "602112233" },
                    { 5, "Fernández", "C/ Princesa 5", "Elena", "603445566" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ProductoId", "Codigo", "Descripcion", "NumeroExistencias", "Precio" },
                values: new object[,]
                {
                    { 1, "P001", "Portátil HP 15\"", 25, 699.99000000000001 },
                    { 2, "P002", "Ratón inalámbrico Logitech", 150, 29.899999999999999 },
                    { 3, "P003", "Teclado mecánico Corsair", 75, 89.989999999999995 },
                    { 4, "P004", "Monitor Samsung 27\"", 40, 229.5 },
                    { 5, "P005", "Impresora HP LaserJet", 30, 179.0 },
                    { 6, "P006", "Auriculares Sony WH-1000XM4", 60, 299.99000000000001 }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "ProveedorId", "Direccion", "Nombre", "Provincia", "Telefono" },
                values: new object[,]
                {
                    { 1, "C/ Mayor 15", "ElectroMax", "Madrid", "911223344" },
                    { 2, "Av. América 22", "TechPlus", "Madrid", "912334455" },
                    { 3, "C/ Gran Vía 120", "Distribuciones Vega", "Toledo", "925123456" },
                    { 4, "Polígono El Olivar", "Logística Sur", "Sevilla", "954998877" }
                });

            migrationBuilder.InsertData(
                table: "Compras",
                columns: new[] { "CompraId", "ClienteId", "FechaCompra", "ProductoId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, 3, new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, 4, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, 5, new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 6, 1, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 7, 3, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, 2, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Suministros",
                columns: new[] { "ProductoId", "ProveedorId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 2, 3 },
                    { 5, 3 },
                    { 4, 4 },
                    { 6, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ClienteId",
                table: "Compras",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ProductoId",
                table: "Compras",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Suministros_ProductoId",
                table: "Suministros",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Suministros");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
