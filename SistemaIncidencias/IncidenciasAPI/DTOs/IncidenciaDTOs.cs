using System.ComponentModel.DataAnnotations;

namespace IncidenciasAPI.DTOs
{
    public class CrearIncidenciaDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [MaxLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public string Categoria { get; set; } = "General";

        [Required(ErrorMessage = "La calle es obligatoria")]
        [MaxLength(150)]
        public string Calle { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Altura { get; set; } = string.Empty; // Número o S/N

        [MaxLength(150)]
        public string? EntreCalles { get; set; }

        [MaxLength(100)]
        public string? Localidad { get; set; } = "Morón";

        public string? ImagenUrl { get; set; }
    }

    public class ActualizarIncidenciaDto
    {
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }
        public string? Calle { get; set; }
        public string? Altura { get; set; }
        public string? EntreCalles { get; set; }
        public string? Localidad { get; set; }
        public string? ImagenUrl { get; set; }
    }

    public class ActualizarEstadoDto
    {
        [Required(ErrorMessage = "El nuevo estado es obligatorio")]
        public string Estado { get; set; } = string.Empty; // "Pendiente", "En Proceso", "Resuelto", "Rechazado"
    }

    public class UsuarioResumenDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class IncidenciaDetalleDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Calle { get; set; } = string.Empty;
        public string Altura { get; set; } = string.Empty;
        public string? EntreCalles { get; set; }
        public string? Localidad { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string? ImagenUrl { get; set; }
        public DateTime FechaReporte { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public UsuarioResumenDto? Usuario { get; set; }
    }
}
