using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlIDMvc.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Creado_por = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProyectoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Horario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horario", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ci = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_nac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Celular = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dirrecion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observaciones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Usuario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contraseña = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sincronizacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image_documento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dirrecion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_inicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Fecha_fin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Telefono = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Dia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HoraInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HorarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dia_Horario_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "Horario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tarjeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    area = table.Column<int>(type: "int", nullable: false),
                    codigo = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Sincronizacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjeta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarjeta_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProyectoId = table.Column<int>(type: "int", nullable: true),
                    Creado_porId = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupo_Persona_Creado_porId",
                        column: x => x.Creado_porId,
                        principalTable: "Persona",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Grupo_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ci = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumTarjeta = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContraseñaTarjeta = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_nac = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Apellido = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Celular = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nota = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dirrecion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    proyecto_idId = table.Column<int>(type: "int", nullable: true),
                    departamento_idId = table.Column<int>(type: "int", nullable: true),
                    grupo_idId = table.Column<int>(type: "int", nullable: true),
                    creado_porId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Departamento_departamento_idId",
                        column: x => x.departamento_idId,
                        principalTable: "Departamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_Grupo_grupo_idId",
                        column: x => x.grupo_idId,
                        principalTable: "Grupo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_Proyecto_proyecto_idId",
                        column: x => x.proyecto_idId,
                        principalTable: "Proyecto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_Usuario_creado_porId",
                        column: x => x.creado_porId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Dia_HorarioId",
                table: "Dia",
                column: "HorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_Creado_porId",
                table: "Grupo",
                column: "Creado_porId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_ProyectoId",
                table: "Grupo",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjeta_PersonaId",
                table: "Tarjeta",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_creado_porId",
                table: "Usuario",
                column: "creado_porId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_departamento_idId",
                table: "Usuario",
                column: "departamento_idId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_grupo_idId",
                table: "Usuario",
                column: "grupo_idId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_proyecto_idId",
                table: "Usuario",
                column: "proyecto_idId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dia");

            migrationBuilder.DropTable(
                name: "Tarjeta");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Horario");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Proyecto");
        }
    }
}
