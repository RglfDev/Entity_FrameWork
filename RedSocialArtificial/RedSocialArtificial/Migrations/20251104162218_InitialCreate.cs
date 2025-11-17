using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RedSocialArtificial.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Datasets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fuente = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datasets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Especializaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especializaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EspecializacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIs_Especializaciones_EspecializacionId",
                        column: x => x.EspecializacionId,
                        principalTable: "Especializaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EspecializacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyectos_Especializaciones_EspecializacionId",
                        column: x => x.EspecializacionId,
                        principalTable: "Especializaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmisorId = table.Column<int>(type: "int", nullable: false),
                    ReceptorId = table.Column<int>(type: "int", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensajes_AIs_EmisorId",
                        column: x => x.EmisorId,
                        principalTable: "AIs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mensajes_AIs_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "AIs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AIProyectos",
                columns: table => new
                {
                    AIEntityId = table.Column<int>(type: "int", nullable: false),
                    ProyectoColaborativoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIProyectos", x => new { x.AIEntityId, x.ProyectoColaborativoId });
                    table.ForeignKey(
                        name: "FK_AIProyectos_AIs_AIEntityId",
                        column: x => x.AIEntityId,
                        principalTable: "AIs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AIProyectos_Proyectos_ProyectoColaborativoId",
                        column: x => x.ProyectoColaborativoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProyectoDatasets",
                columns: table => new
                {
                    ProyectoColaborativoId = table.Column<int>(type: "int", nullable: false),
                    DatasetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectoDatasets", x => new { x.ProyectoColaborativoId, x.DatasetId });
                    table.ForeignKey(
                        name: "FK_ProyectoDatasets_Datasets_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Datasets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProyectoDatasets_Proyectos_ProyectoColaborativoId",
                        column: x => x.ProyectoColaborativoId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Datasets",
                columns: new[] { "Id", "Descripcion", "Fuente", "Nombre" },
                values: new object[,]
                {
                    { 1, "Conjunto de imágenes etiquetadas.", "imagenet.org", "Imagenet Subset" },
                    { 2, "Artículos de Wikipedia para NLP.", "wikipedia.org", "Wikipedia Corpus" },
                    { 3, "Datos financieros históricos.", "kaggle.com", "Kaggle Finance" }
                });

            migrationBuilder.InsertData(
                table: "Especializaciones",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Visión Artificial" },
                    { 2, "Procesamiento de Lenguaje Natural" },
                    { 3, "Análisis de Datos" }
                });

            migrationBuilder.InsertData(
                table: "AIs",
                columns: new[] { "Id", "Descripcion", "EspecializacionId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Especialista en reconocimiento de imágenes.", 1, "VisionAI" },
                    { 2, "Procesamiento de texto y lenguaje natural.", 2, "LangMind" },
                    { 3, "Analista de grandes volúmenes de datos.", 3, "DataCrunch" }
                });

            migrationBuilder.InsertData(
                table: "Proyectos",
                columns: new[] { "Id", "Descripcion", "EspecializacionId", "Titulo" },
                values: new object[,]
                {
                    { 1, "Red neuronal para clasificación de imágenes.", 1, "Proyecto VisualNet" },
                    { 2, "Modelo de diálogo y comprensión contextual.", 2, "Proyecto ChatNLP" },
                    { 3, "Análisis predictivo de series temporales.", 3, "Proyecto DataInsight" }
                });

            migrationBuilder.InsertData(
                table: "AIProyectos",
                columns: new[] { "AIEntityId", "ProyectoColaborativoId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Mensajes",
                columns: new[] { "Id", "Contenido", "EmisorId", "FechaEnvio", "ReceptorId" },
                values: new object[,]
                {
                    { 1, "¿Podrías ayudarme a generar descripciones de imágenes?", 1, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2, "Claro, puedo procesar las etiquetas y crear textos naturales.", 2, new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, "Tengo nuevos datos de rendimiento para tu red visual.", 3, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "ProyectoDatasets",
                columns: new[] { "DatasetId", "ProyectoColaborativoId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIProyectos_ProyectoColaborativoId",
                table: "AIProyectos",
                column: "ProyectoColaborativoId");

            migrationBuilder.CreateIndex(
                name: "IX_AIs_EspecializacionId",
                table: "AIs",
                column: "EspecializacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_EmisorId",
                table: "Mensajes",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_ReceptorId",
                table: "Mensajes",
                column: "ReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectoDatasets_DatasetId",
                table: "ProyectoDatasets",
                column: "DatasetId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_EspecializacionId",
                table: "Proyectos",
                column: "EspecializacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIProyectos");

            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "ProyectoDatasets");

            migrationBuilder.DropTable(
                name: "AIs");

            migrationBuilder.DropTable(
                name: "Datasets");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Especializaciones");
        }
    }
}
