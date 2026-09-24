using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncidenciasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlignCurrentSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "usuario");

            migrationBuilder.RenameColumn(
                name: "Rol",
                table: "usuario",
                newName: "rol");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "usuario",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "usuario",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "NombreCompleto",
                table: "usuario",
                newName: "nombre_completo");

            migrationBuilder.RenameColumn(
                name: "FechaRegistro",
                table: "usuario",
                newName: "fecha_registro");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "usuario",
                newName: "id_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Email",
                table: "usuario",
                newName: "IX_usuario_email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario",
                table: "usuario",
                column: "id_usuario");

            migrationBuilder.CreateTable(
                name: "ciudadano",
                columns: table => new
                {
                    id_ciudadano = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dni = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    localidad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ciudadano", x => x.id_ciudadano);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario_repo",
                columns: table => new
                {
                    id_usuario_repo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha_repo = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    lugar_repo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado_repo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_repo", x => x.id_usuario_repo);
                    table.ForeignKey(
                        name: "FK_usuario_repo_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "incidente",
                columns: table => new
                {
                    id_incidente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha_inicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_finalizacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ubicacion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    id_ciudadano = table.Column<int>(type: "int", nullable: true),
                    id_usuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incidente", x => x.id_incidente);
                    table.ForeignKey(
                        name: "FK_incidente_ciudadano_id_ciudadano",
                        column: x => x.id_ciudadano,
                        principalTable: "ciudadano",
                        principalColumn: "id_ciudadano",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_incidente_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "alerta_inci",
                columns: table => new
                {
                    id_alerta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    fecha_alerta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    mensaje = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_incidente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alerta_inci", x => x.id_alerta);
                    table.ForeignKey(
                        name: "FK_alerta_inci_incidente_id_incidente",
                        column: x => x.id_incidente,
                        principalTable: "incidente",
                        principalColumn: "id_incidente",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reporte",
                columns: table => new
                {
                    id_reporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    lugar = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado_reporte = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_incidente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reporte", x => x.id_reporte);
                    table.ForeignKey(
                        name: "FK_reporte_incidente_id_incidente",
                        column: x => x.id_incidente,
                        principalTable: "incidente",
                        principalColumn: "id_incidente",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_alerta_inci_id_incidente",
                table: "alerta_inci",
                column: "id_incidente");

            migrationBuilder.CreateIndex(
                name: "IX_incidente_id_ciudadano",
                table: "incidente",
                column: "id_ciudadano");

            migrationBuilder.CreateIndex(
                name: "IX_incidente_id_usuario",
                table: "incidente",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_reporte_id_incidente",
                table: "reporte",
                column: "id_incidente");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_repo_id_usuario",
                table: "usuario_repo",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alerta_inci");

            migrationBuilder.DropTable(
                name: "reporte");

            migrationBuilder.DropTable(
                name: "usuario_repo");

            migrationBuilder.DropTable(
                name: "incidente");

            migrationBuilder.DropTable(
                name: "ciudadano");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario",
                table: "usuario");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "rol",
                table: "Usuarios",
                newName: "Rol");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Usuarios",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "nombre_completo",
                table: "Usuarios",
                newName: "NombreCompleto");

            migrationBuilder.RenameColumn(
                name: "fecha_registro",
                table: "Usuarios",
                newName: "FechaRegistro");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_email",
                table: "Usuarios",
                newName: "IX_Usuarios_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Categoria = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FechaReporte = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ImagenUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion_Altura = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion_Calle = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion_EntreCalles = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion_Localidad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidencias_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_UsuarioId",
                table: "Incidencias",
                column: "UsuarioId");
        }
    }
}
