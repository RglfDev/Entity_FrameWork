using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RubenAppFinal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entrenadores",
                columns: table => new
                {
                    EntrenadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrenadores", x => x.EntrenadorId);
                });

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    SocioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.SocioId);
                });

            migrationBuilder.CreateTable(
                name: "Clases",
                columns: table => new
                {
                    ClaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nivel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioMensual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntrenadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clases", x => x.ClaseId);
                    table.ForeignKey(
                        name: "FK_Clases_Entrenadores_EntrenadorId",
                        column: x => x.EntrenadorId,
                        principalTable: "Entrenadores",
                        principalColumn: "EntrenadorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    InscripcionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    ClaseId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.InscripcionId);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Clases_ClaseId",
                        column: x => x.ClaseId,
                        principalTable: "Clases",
                        principalColumn: "ClaseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Socios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socios",
                        principalColumn: "SocioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Entrenadores",
                columns: new[] { "EntrenadorId", "Especialidad", "Nombre" },
                values: new object[,]
                {
                    { 1, "CrossFit", "Carlos Ruiz" },
                    { 2, "Yoga", "Laura Sánchez" },
                    { 3, "Boxeo", "David López" }
                });

            migrationBuilder.InsertData(
                table: "Socios",
                columns: new[] { "SocioId", "Email", "Nombre" },
                values: new object[,]
                {
                    { 1, "carlos@email.com", "Carlos Pérez" },
                    { 2, "ana@email.com", "Ana Gómez" },
                    { 3, "luis@email.com", "Luis Martínez" },
                    { 4, "marta@email.com", "Marta Díaz" },
                    { 5, "javier@email.com", "Javier Torres" },
                    { 6, "lucia@email.com", "Lucía Fernández" },
                    { 7, "pedro@email.com", "Pedro Morales" }
                });

            migrationBuilder.InsertData(
                table: "Clases",
                columns: new[] { "ClaseId", "EntrenadorId", "Nivel", "Nombre", "PrecioMensual" },
                values: new object[,]
                {
                    { 1, 1, "Avanzado", "CrossFit Avanzado", 50m },
                    { 2, 2, "Básico", "Yoga Principiantes", 30m },
                    { 3, 3, "Intermedio", "Boxeo Intermedio", 40m },
                    { 4, 2, "Básico", "Pilates", 35m }
                });

            migrationBuilder.InsertData(
                table: "Inscripciones",
                columns: new[] { "InscripcionId", "ClaseId", "FechaInicio", "SocioId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 2, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 6, 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 7, 2, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 8, 4, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 9, 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 10, 4, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 11, 2, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 12, 3, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 13, 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clases_EntrenadorId",
                table: "Clases",
                column: "EntrenadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_ClaseId",
                table: "Inscripciones",
                column: "ClaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_SocioId",
                table: "Inscripciones",
                column: "SocioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.DropTable(
                name: "Clases");

            migrationBuilder.DropTable(
                name: "Socios");

            migrationBuilder.DropTable(
                name: "Entrenadores");
        }
    }
}
