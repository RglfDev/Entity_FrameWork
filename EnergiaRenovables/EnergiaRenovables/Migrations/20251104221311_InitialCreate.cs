using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnergiaRenovables.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inversores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapitalDisponible = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inversores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEnergias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEnergias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InversionTotal = table.Column<double>(type: "float", nullable: false),
                    TipoEnergiaId = table.Column<int>(type: "int", nullable: false),
                    UbicacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyectos_TipoEnergias_TipoEnergiaId",
                        column: x => x.TipoEnergiaId,
                        principalTable: "TipoEnergias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proyectos_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Informes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnergiaGeneradaMWh = table.Column<double>(type: "float", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Informes_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inversiones",
                columns: table => new
                {
                    InversorId = table.Column<int>(type: "int", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    MontoInvertido = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inversiones", x => new { x.InversorId, x.ProyectoId });
                    table.ForeignKey(
                        name: "FK_Inversiones_Inversores_InversorId",
                        column: x => x.InversorId,
                        principalTable: "Inversores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inversiones_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Inversores",
                columns: new[] { "Id", "CapitalDisponible", "Nombre" },
                values: new object[,]
                {
                    { 1, 2000000.0, "Inversiones Globales S.A." },
                    { 2, 1500000.0, "EcoCapital" },
                    { 3, 1800000.0, "GreenFund" },
                    { 4, 1200000.0, "SolarTrust" },
                    { 5, 2200000.0, "BlueEnergy" }
                });

            migrationBuilder.InsertData(
                table: "TipoEnergias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Solar" },
                    { 2, "Eólica" },
                    { 3, "Hidráulica" },
                    { 4, "Geotérmica" },
                    { 5, "Biomasa" }
                });

            migrationBuilder.InsertData(
                table: "Ubicaciones",
                columns: new[] { "Id", "Ciudad", "Pais" },
                values: new object[,]
                {
                    { 1, "Sevilla", "España" },
                    { 2, "Hamburgo", "Alemania" },
                    { 3, "Santiago", "Chile" },
                    { 4, "Lisboa", "Portugal" },
                    { 5, "Toronto", "Canadá" }
                });

            migrationBuilder.InsertData(
                table: "Proyectos",
                columns: new[] { "Id", "InversionTotal", "Nombre", "TipoEnergiaId", "UbicacionId" },
                values: new object[,]
                {
                    { 1, 500000.0, "Solar Sur", 1, 1 },
                    { 2, 850000.0, "Viento Norte", 2, 2 },
                    { 3, 1200000.0, "Río Verde", 3, 3 },
                    { 4, 650000.0, "Calor Tierra", 4, 4 },
                    { 5, 900000.0, "BioFuturo", 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Informes",
                columns: new[] { "Id", "EnergiaGeneradaMWh", "Fecha", "ProyectoId" },
                values: new object[,]
                {
                    { 1, 1200.0, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2300.0, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 3100.0, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, 1800.0, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, 2600.0, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 }
                });

            migrationBuilder.InsertData(
                table: "Inversiones",
                columns: new[] { "InversorId", "ProyectoId", "MontoInvertido" },
                values: new object[,]
                {
                    { 1, 1, 250000.0 },
                    { 2, 2, 400000.0 },
                    { 3, 3, 600000.0 },
                    { 4, 4, 300000.0 },
                    { 5, 5, 500000.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Informes_ProyectoId",
                table: "Informes",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inversiones_ProyectoId",
                table: "Inversiones",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_TipoEnergiaId",
                table: "Proyectos",
                column: "TipoEnergiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_UbicacionId",
                table: "Proyectos",
                column: "UbicacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Informes");

            migrationBuilder.DropTable(
                name: "Inversiones");

            migrationBuilder.DropTable(
                name: "Inversores");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "TipoEnergias");

            migrationBuilder.DropTable(
                name: "Ubicaciones");
        }
    }
}
