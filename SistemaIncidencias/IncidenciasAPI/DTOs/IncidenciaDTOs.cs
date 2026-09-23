using System.ComponentModel.DataAnnotations;

namespace IncidenciasAPI.DTOs
{
    /// <summary>DTO para crear un nuevo incidente.</summary>
    public class CrearIncidenteDto
    {
        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Ubicacion { get; set; }

        /// <summary>ID del ciudadano que reporta (opcional).</summary>
        public int? IdCiudadano { get; set; }
    }

    /// <summary>DTO para actualizar campos de un incidente.</summary>
    public class ActualizarIncidenteDto
    {
        public string? Descripcion { get; set; }
        public string? Ubicacion  { get; set; }
        public string? Estado     { get; set; } // Pendiente | EnProceso | Resuelto
    }

    /// <summary>DTO para cambiar solo el estado de un incidente.</summary>
    public class ActualizarEstadoDto
    {
        [Required(ErrorMessage = "El nuevo estado es obligatorio")]
        public string Estado { get; set; } = string.Empty;
    }

    /// <summary>Resumen de usuario en respuestas de incidente.</summary>
    public class UsuarioResumenDto
    {
        public int    IdUsuario      { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email          { get; set; } = string.Empty;
    }

    /// <summary>Resumen de ciudadano en respuestas de incidente.</summary>
    public class CiudadanoResumenDto
    {
        public int     IdCiudadano { get; set; }
        public string  Nombre      { get; set; } = string.Empty;
        public string? Apellido    { get; set; }
        public string  Dni         { get; set; } = string.Empty;
    }

    /// <summary>DTO completo de un incidente para respuestas de la API.</summary>
    public class IncidenteDetalleDto
    {
        public int      IdIncidente      { get; set; }
        public string   Descripcion      { get; set; } = string.Empty;
        public string?  Ubicacion        { get; set; }
        public string   Estado           { get; set; } = string.Empty;
        public DateTime FechaInicio      { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public UsuarioResumenDto?   Usuario   { get; set; }
        public CiudadanoResumenDto? Ciudadano { get; set; }
    }
}
