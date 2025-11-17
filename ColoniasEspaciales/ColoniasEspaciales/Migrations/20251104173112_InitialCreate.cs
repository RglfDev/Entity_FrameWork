using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ColoniasEspaciales.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemperaturaPromedio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colonias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanetaId = table.Column<int>(type: "int", nullable: false),
                    NivelSostenibilidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colonias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colonias_Planetas_PlanetaId",
                        column: x => x.PlanetaId,
                        principalTable: "Planetas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ColoniaRecursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColoniaId = table.Column<int>(type: "int", nullable: false),
                    RecursoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColoniaRecursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColoniaRecursos_Colonias_ColoniaId",
                        column: x => x.ColoniaId,
                        principalTable: "Colonias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ColoniaRecursos_Recursos_RecursoId",
                        column: x => x.RecursoId,
                        principalTable: "Recursos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ColoniaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Colonias_ColoniaId",
                        column: x => x.ColoniaId,
                        principalTable: "Colonias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Habitantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColoniaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habitantes_Colonias_ColoniaId",
                        column: x => x.ColoniaId,
                        principalTable: "Colonias",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Planetas",
                columns: new[] { "Id", "Nombre", "TemperaturaPromedio", "Tipo" },
                values: new object[,]
                {
                    { 1, "Aurelia", 22.5, "Terrestre" },
                    { 2, "Zephyria", -80.0, "Gaseoso" }
                });

            migrationBuilder.InsertData(
                table: "Recursos",
                columns: new[] { "Id", "CantidadTotal", "Nombre" },
                values: new object[,]
                {
                    { 1, 10000, "Agua" },
                    { 2, 8000, "Oxígeno" },
                    { 3, 12000, "Metal" }
                });

            migrationBuilder.InsertData(
                table: "Colonias",
                columns: new[] { "Id", "NivelSostenibilidad", "Nombre", "PlanetaId" },
                values: new object[,]
                {
                    { 1, "Alta", "Nova Terra", 1 },
                    { 2, "Media", "Viento Azul", 2 }
                });

            migrationBuilder.InsertData(
                table: "ColoniaRecursos",
                columns: new[] { "Id", "Cantidad", "ColoniaId", "RecursoId" },
                values: new object[,]
                {
                    { 1, 5000, 1, 1 },
                    { 2, 4000, 1, 2 },
                    { 3, 3000, 2, 1 },
                    { 4, 6000, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "Id", "ColoniaId", "Descripcion", "Fecha", "Tipo" },
                values: new object[,]
                {
                    { 1, 1, "Una tormenta afecta las comunicaciones.", new DateTime(2124, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tormenta Solar" },
                    { 2, 2, "Se ha encontrado un nuevo mineral.", new DateTime(2124, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Descubrimiento" }
                });

            migrationBuilder.InsertData(
                table: "Habitantes",
                columns: new[] { "Id", "ColoniaId", "Nombre", "Rol" },
                values: new object[,]
                {
                    { 1, 1, "Lucía Vega", "Comandante" },
                    { 2, 1, "Carlos Luna", "Ingeniero" },
                    { 3, 2, "Marta Sol", "Bióloga" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColoniaRecursos_ColoniaId",
                table: "ColoniaRecursos",
                column: "ColoniaId");

            migrationBuilder.CreateIndex(
                name: "IX_ColoniaRecursos_RecursoId",
                table: "ColoniaRecursos",
                column: "RecursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Colonias_PlanetaId",
                table: "Colonias",
                column: "PlanetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_ColoniaId",
                table: "Eventos",
                column: "ColoniaId");

            migrationBuilder.CreateIndex(
                name: "IX_Habitantes_ColoniaId",
                table: "Habitantes",
                column: "ColoniaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColoniaRecursos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Habitantes");

            migrationBuilder.DropTable(
                name: "Recursos");

            migrationBuilder.DropTable(
                name: "Colonias");

            migrationBuilder.DropTable(
                name: "Planetas");
        }
    }
}
