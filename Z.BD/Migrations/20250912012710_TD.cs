using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Z.BD.Migrations
{
    /// <inheritdoc />
    public partial class TD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Obras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Rubro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obras_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Presupuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Rubro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presupuestos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemObra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObraId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemObra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemObra_Obras_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemObraPresupuesto",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "int", nullable: false),
                    PresupuestosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemObraPresupuesto", x => new { x.ItemsId, x.PresupuestosId });
                    table.ForeignKey(
                        name: "FK_ItemObraPresupuesto_ItemObra_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "ItemObra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemObraPresupuesto_Presupuestos_PresupuestosId",
                        column: x => x.PresupuestosId,
                        principalTable: "Presupuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemObra_ObraId",
                table: "ItemObra",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemObraPresupuesto_PresupuestosId",
                table: "ItemObraPresupuesto",
                column: "PresupuestosId");

            migrationBuilder.CreateIndex(
                name: "IX_Obras_ClienteId",
                table: "Obras",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_ClienteId",
                table: "Presupuestos",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemObraPresupuesto");

            migrationBuilder.DropTable(
                name: "ItemObra");

            migrationBuilder.DropTable(
                name: "Presupuestos");

            migrationBuilder.DropTable(
                name: "Obras");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
