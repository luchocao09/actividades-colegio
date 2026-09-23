using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncidenciasAPI.Models
{
    /// <summary>
    /// Representa un incidente cargado en el sistema.
    /// Tabla: incidente
    /// </summary>
    [Table("incidente")]
    public class Incidente
    {
        [Key]
        [Column("id_incidente")]
        public int IdIncidente { get; set; }

        [Column("fecha_inicio")]
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;

        [Column("fecha_finalizacion")]
        public DateTime? FechaFinalizacion { get; set; }

        [Required]
        [MaxLength(500)]
        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [MaxLength(200)]
        [Column("ubicacion")]
        public string? Ubicacion { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("estado")]
        public string Estado { get; set; } = "Pendiente"; // Pendiente | EnProceso | Resuelto

        /// <summary>
        /// Indica si la incidencia fue eliminada de forma lógica (soft delete).
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        // ---- Claves foráneas ----

        /// <summary>FK hacia ciudadano (quien reportó el incidente).</summary>
        [Column("id_ciudadano")]
        public int? IdCiudadano { get; set; }

        /// <summary>FK hacia el usuario del sistema responsable.</summary>
        [Column("id_usuario")]
        public int? IdUsuario { get; set; }

        // ---- Navegación ----

        [ForeignKey("IdCiudadano")]
        [JsonIgnore]
        public Ciudadano? Ciudadano { get; set; }

        [ForeignKey("IdUsuario")]
        [JsonIgnore]
        public Usuario? Usuario { get; set; }

        [JsonIgnore]
        public ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();

        [JsonIgnore]
        public ICollection<AlertaIncidente> Alertas { get; set; } = new List<AlertaIncidente>();
    }
}
