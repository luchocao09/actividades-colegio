using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncidenciasAPI.Models
{
    /// <summary>
    /// Historial de acciones de un usuario sobre incidentes (usuario repositor).
    /// Tabla: usuario_repo
    /// </summary>
    [Table("usuario_repo")]
    public class UsuarioRepo
    {
        [Key]
        [Column("id_usuario_repo")]
        public int IdUsuarioRepo { get; set; }

        [Column("fecha_repo")]
        public DateTime FechaRepo { get; set; } = DateTime.UtcNow;

        [MaxLength(200)]
        [Column("lugar_repo")]
        public string? LugarRepo { get; set; }

        [MaxLength(50)]
        [Column("estado_repo")]
        public string EstadoRepo { get; set; } = "Activo";

        // FK al usuario del sistema
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }
}
