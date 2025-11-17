using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReservaHoteles.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoIVA = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreClienteAgencia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Hoteles",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaConstruccion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteles", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hoteles_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Habitaciones",
                columns: table => new
                {
                    HabitacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoHabitacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitaciones", x => x.HabitacionId);
                    table.ForeignKey(
                        name: "FK_Habitaciones_Hoteles_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hoteles",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    HabitacionId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK_Reservas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservas_Habitaciones_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitaciones",
                        principalColumn: "HabitacionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Descripcion", "TipoIVA" },
                values: new object[,]
                {
                    { 1, "Tres Estrellas", 10.0 },
                    { 2, "Cuatro Estrellas", 10.0 },
                    { 3, "Cinco Estrellas", 21.0 }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "Direccion", "Nombre", "NombreClienteAgencia", "Telefono", "TipoCliente" },
                values: new object[,]
                {
                    { 1, "Calle Luna 5", "Carlos Gómez", null, "600112233", "Particular" },
                    { 2, "Av. Sol 23", "Laura Pérez", null, "600223344", "Particular" },
                    { 3, "Calle Flor 8", "Antonio Ruiz", null, "600334455", "Particular" },
                    { 4, "Plaza Norte 4", "María López", null, "600445566", "Particular" },
                    { 5, "Calle Prado 7", "Pedro Jiménez", null, "600556677", "Particular" },
                    { 6, "Calle Comercio 22", "Viajes SolTour", "Marta Díaz", "910223344", "Agencia" },
                    { 7, "Av. Europa 18", "Aventuras Travel", "Javier Cano", "910334455", "Agencia" },
                    { 8, "Calle Jardín 9", "HolidayExpress", "Lucía Torres", "910445566", "Agencia" },
                    { 9, "Av. Central 12", "GlobalTour", "David Martín", "910556677", "Agencia" },
                    { 10, "Paseo del Arte 3", "TravelPro", "Elena Ramos", "910667788", "Agencia" }
                });

            migrationBuilder.InsertData(
                table: "Hoteles",
                columns: new[] { "HotelId", "CategoriaId", "Direccion", "FechaConstruccion", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, 1, "Calle Mayor 10", new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hotel Sol", "911223344" },
                    { 2, 3, "Av. Castellana 200", new DateTime(2005, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gran Palace", "912334455" },
                    { 3, 2, "Paseo del Mar 22", new DateTime(2010, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Costa Azul", "933556677" }
                });

            migrationBuilder.InsertData(
                table: "Habitaciones",
                columns: new[] { "HabitacionId", "HotelId", "Numero", "TipoHabitacion" },
                values: new object[,]
                {
                    { 1, 1, 101, "Individual" },
                    { 2, 1, 102, "Doble" },
                    { 3, 2, 201, "Suite" },
                    { 4, 2, 202, "Doble" },
                    { 5, 3, 301, "Individual" },
                    { 6, 3, 302, "Suite" }
                });

            migrationBuilder.InsertData(
                table: "Reservas",
                columns: new[] { "ReservaId", "ClienteId", "FechaFin", "FechaInicio", "HabitacionId", "Precio" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 400.0 },
                    { 2, 1, new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 420.0 },
                    { 3, 2, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 250.0 },
                    { 4, 3, new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 700.0 },
                    { 5, 4, new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 300.0 },
                    { 6, 5, new DateTime(2024, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 500.0 },
                    { 7, 2, new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 450.0 },
                    { 8, 3, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 520.0 },
                    { 9, 6, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 350.0 },
                    { 10, 6, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 600.0 },
                    { 11, 7, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 600.0 },
                    { 12, 8, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 950.0 },
                    { 13, 8, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 980.0 },
                    { 14, 9, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 800.0 },
                    { 15, 10, new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 280.0 },
                    { 16, 7, new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 310.0 },
                    { 17, 8, new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 400.0 },
                    { 18, 9, new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 390.0 },
                    { 19, 6, new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 500.0 },
                    { 20, 10, new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 720.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habitaciones_HotelId",
                table: "Habitaciones",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteles_CategoriaId",
                table: "Hoteles",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteId",
                table: "Reservas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_HabitacionId",
                table: "Reservas",
                column: "HabitacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Habitaciones");

            migrationBuilder.DropTable(
                name: "Hoteles");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
