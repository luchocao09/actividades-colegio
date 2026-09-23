using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncidenciasAPI.Models
{
    /// <summary>
    /// Representa a un ciudadano que puede reportar incidentes.
    /// Tabla: ciudadano
    /// </summary>
    [Table("ciudadano")]
    public class Ciudadano
    {
        [Key]
        [Column("id_ciudadano")]
        public int IdCiudadano { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("dni")]
        public string Dni { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("apellido")]
        public string? Apellido { get; set; }

        [MaxLength(100)]
        [Column("localidad")]
        public string? Localidad { get; set; }

        [MaxLength(150)]
        [Column("email")]
        public string? Email { get; set; }

        [MaxLength(30)]
        [Column("telefono")]
        public string? Telefono { get; set; }

        // Navegación: un ciudadano puede tener varios incidentes
        [JsonIgnore]
        public ICollection<Incidente> Incidentes { get; set; } = new List<Incidente>();
    }
}
