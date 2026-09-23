using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncidenciasAPI.Models
{
    /// <summary>
    /// Registro de actualizaciones o seguimiento de un incidente.
    /// Tabla: reporte
    /// </summary>
    [Table("reporte")]
    public class Reporte
    {
        [Key]
        [Column("id_reporte")]
        public int IdReporte { get; set; }

        [Required]
        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [Column("fecha")]
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        [MaxLength(200)]
        [Column("lugar")]
        public string? Lugar { get; set; }

        [MaxLength(50)]
        [Column("estado_reporte")]
        public string EstadoReporte { get; set; } = "Enviado";

        // FK
        [Column("id_incidente")]
        public int IdIncidente { get; set; }

        [ForeignKey("IdIncidente")]
        [JsonIgnore]
        public Incidente? Incidente { get; set; }
    }
}
