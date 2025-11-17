using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSocialIA.Migrations
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
                    DatasetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datasets", x => x.DatasetId);
                });

            migrationBuilder.CreateTable(
                name: "Especializaciones",
                columns: table => new
                {
                    EspecializacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especializaciones", x => x.EspecializacionID);
                });

            migrationBuilder.CreateTable(
                name: "ProyectosColaborativos",
                columns: table => new
                {
                    ProyectoColaborativoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectosColaborativos", x => x.ProyectoColaborativoId);
                });

            migrationBuilder.CreateTable(
                name: "AIs",
                columns: table => new
                {
                    AiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NivelInteligencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EspecializacionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIs", x => x.AiId);
                    table.ForeignKey(
                        name: "FK_AIs_Especializaciones_EspecializacionID",
                        column: x => x.EspecializacionID,
                        principalTable: "Especializaciones",
                        principalColumn: "EspecializacionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatasetProyectoColaborativo",
                columns: table => new
                {
                    DatasetsDatasetId = table.Column<int>(type: "int", nullable: false),
                    ProyectosColaborativosProyectoColaborativoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatasetProyectoColaborativo", x => new { x.DatasetsDatasetId, x.ProyectosColaborativosProyectoColaborativoId });
                    table.ForeignKey(
                        name: "FK_DatasetProyectoColaborativo_Datasets_DatasetsDatasetId",
                        column: x => x.DatasetsDatasetId,
                        principalTable: "Datasets",
                        principalColumn: "DatasetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatasetProyectoColaborativo_ProyectosColaborativos_ProyectosColaborativosProyectoColaborativoId",
                        column: x => x.ProyectosColaborativosProyectoColaborativoId,
                        principalTable: "ProyectosColaborativos",
                        principalColumn: "ProyectoColaborativoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AIProyectoColaborativo",
                columns: table => new
                {
                    AIsAiId = table.Column<int>(type: "int", nullable: false),
                    ProyectosColaborativosProyectoColaborativoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIProyectoColaborativo", x => new { x.AIsAiId, x.ProyectosColaborativosProyectoColaborativoId });
                    table.ForeignKey(
                        name: "FK_AIProyectoColaborativo_AIs_AIsAiId",
                        column: x => x.AIsAiId,
                        principalTable: "AIs",
                        principalColumn: "AiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AIProyectoColaborativo_ProyectosColaborativos_ProyectosColaborativosProyectoColaborativoId",
                        column: x => x.ProyectosColaborativosProyectoColaborativoId,
                        principalTable: "ProyectosColaborativos",
                        principalColumn: "ProyectoColaborativoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    MensajeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajes", x => x.MensajeId);
                    table.ForeignKey(
                        name: "FK_Mensajes_AIs_AiId",
                        column: x => x.AiId,
                        principalTable: "AIs",
                        principalColumn: "AiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIProyectoColaborativo_ProyectosColaborativosProyectoColaborativoId",
                table: "AIProyectoColaborativo",
                column: "ProyectosColaborativosProyectoColaborativoId");

            migrationBuilder.CreateIndex(
                name: "IX_AIs_EspecializacionID",
                table: "AIs",
                column: "EspecializacionID");

            migrationBuilder.CreateIndex(
                name: "IX_DatasetProyectoColaborativo_ProyectosColaborativosProyectoColaborativoId",
                table: "DatasetProyectoColaborativo",
                column: "ProyectosColaborativosProyectoColaborativoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_AiId",
                table: "Mensajes",
                column: "AiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIProyectoColaborativo");

            migrationBuilder.DropTable(
                name: "DatasetProyectoColaborativo");

            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "Datasets");

            migrationBuilder.DropTable(
                name: "ProyectosColaborativos");

            migrationBuilder.DropTable(
                name: "AIs");

            migrationBuilder.DropTable(
                name: "Especializaciones");
        }
    }
}
