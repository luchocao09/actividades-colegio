using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IncidenciasAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Rol { get; set; } = "Ciudadano"; // "Ciudadano" o "Administrador"

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Relación: Un usuario puede tener múltiples incidencias reportadas
        [JsonIgnore]
        public ICollection<Incidencia> Incidencias { get; set; } = new List<Incidencia>();
    }
}
