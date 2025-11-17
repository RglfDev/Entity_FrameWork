using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CriptomonedasEF.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Criptomonedas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Simbolo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorActual = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criptomonedas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaldoVirtual = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoOperacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    CriptomonedaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operaciones_Criptomonedas_CriptomonedaId",
                        column: x => x.CriptomonedaId,
                        principalTable: "Criptomonedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Portafolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    CriptomonedaId = table.Column<int>(type: "int", nullable: false),
                    CantidadActual = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portafolios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portafolios_Criptomonedas_CriptomonedaId",
                        column: x => x.CriptomonedaId,
                        principalTable: "Criptomonedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Portafolios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Criptomonedas",
                columns: new[] { "Id", "Nombre", "Simbolo", "ValorActual" },
                values: new object[,]
                {
                    { 1, "Bitcoin", "BTC", 65000.0 },
                    { 2, "Ethereum", "ETH", 3400.5 },
                    { 3, "Cardano", "ADA", 0.45000000000000001 },
                    { 4, "Solana", "SOL", 180.75 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Nombre", "SaldoVirtual" },
                values: new object[,]
                {
                    { 1, "alice@crypto.com", "Alice", 2500.0 },
                    { 2, "bob@crypto.com", "Bob", 1200.5 },
                    { 3, "charlie@crypto.com", "Charlie", 3400.75 }
                });

            migrationBuilder.InsertData(
                table: "Operaciones",
                columns: new[] { "Id", "Cantidad", "CriptomonedaId", "Fecha", "TipoOperacion", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 0.029999999999999999, 1, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compra", 1 },
                    { 2, 1.0, 3, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Venta", 2 },
                    { 3, 2.0, 2, new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compra", 3 },
                    { 4, 0.070000000000000007, 1, new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compra", 3 }
                });

            migrationBuilder.InsertData(
                table: "Portafolios",
                columns: new[] { "Id", "CantidadActual", "CriptomonedaId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 0.050000000000000003, 1, 1 },
                    { 2, 2.2999999999999998, 2, 1 },
                    { 3, 1000.0, 3, 2 },
                    { 4, 5.5, 4, 3 },
                    { 5, 0.12, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_CriptomonedaId",
                table: "Operaciones",
                column: "CriptomonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_UsuarioId",
                table: "Operaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Portafolios_CriptomonedaId",
                table: "Portafolios",
                column: "CriptomonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Portafolios_UsuarioId",
                table: "Portafolios",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operaciones");

            migrationBuilder.DropTable(
                name: "Portafolios");

            migrationBuilder.DropTable(
                name: "Criptomonedas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
