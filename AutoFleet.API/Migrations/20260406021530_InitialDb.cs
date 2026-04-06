using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoFleet.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conductores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LicenciaNumero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LicenciaCategoria = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LicenciaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoDisponibilidad = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conductores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rutas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Origen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Destino = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Distancia = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Duracion = table.Column<TimeSpan>(type: "time", nullable: false),
                    TipoServicio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TipoVehiculo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Año = table.Column<int>(type: "int", nullable: false),
                    CapacidadCargaKg = table.Column<float>(type: "real", nullable: false),
                    EstadoOperativo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    KilometrajeActual = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CargasCombustible",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadCombustible = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kilometraje = table.Column<int>(type: "int", nullable: false),
                    Gasolinera = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargasCombustible", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargasCombustible_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntidadResponsable = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Detalles = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProximoMantenimiento = table.Column<DateTime>(type: "date", nullable: true),
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recorridos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraSalida = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraLlegada = table.Column<TimeOnly>(type: "time", nullable: false),
                    KmInicial = table.Column<int>(type: "int", maxLength: 7, nullable: false),
                    KmFinal = table.Column<int>(type: "int", maxLength: 7, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RutaId = table.Column<int>(type: "int", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    ConductorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recorridos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recorridos_Conductores_ConductorId",
                        column: x => x.ConductorId,
                        principalTable: "Conductores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recorridos_Rutas_RutaId",
                        column: x => x.RutaId,
                        principalTable: "Rutas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recorridos_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargasCombustible_VehiculoId",
                table: "CargasCombustible",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_VehiculoId",
                table: "Mantenimientos",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Recorridos_ConductorId",
                table: "Recorridos",
                column: "ConductorId");

            migrationBuilder.CreateIndex(
                name: "IX_Recorridos_RutaId",
                table: "Recorridos",
                column: "RutaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recorridos_VehiculoId",
                table: "Recorridos",
                column: "VehiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargasCombustible");

            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Recorridos");

            migrationBuilder.DropTable(
                name: "Conductores");

            migrationBuilder.DropTable(
                name: "Rutas");

            migrationBuilder.DropTable(
                name: "Vehiculos");
        }
    }
}
