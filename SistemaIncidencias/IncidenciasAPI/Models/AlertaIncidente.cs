using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncidenciasAPI.Models
{
    /// <summary>
    /// Alerta asociada a un incidente activo.
    /// Tabla: alerta_inci
    /// </summary>
    [Table("alerta_inci")]
    public class AlertaIncidente
    {
        [Key]
        [Column("id_alerta")]
        public int IdAlerta { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [Column("fecha_alerta")]
        public DateTime FechaAlerta { get; set; } = DateTime.UtcNow;

        [MaxLength(300)]
        [Column("mensaje")]
        public string? Mensaje { get; set; }

        // FK al incidente al que pertenece la alerta
        [Column("id_incidente")]
        public int IdIncidente { get; set; }

        [ForeignKey("IdIncidente")]
        [JsonIgnore]
        public Incidente? Incidente { get; set; }
    }
}
