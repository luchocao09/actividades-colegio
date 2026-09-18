using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IncidenciasAPI.Models
{
    /// <summary>
    /// Representa una incidencia reportada por un ciudadano.
    /// </summary>
    public class Incidencia
    {
        #region Identificador
        /// <summary>
        /// Identificador único de la incidencia.
        /// </summary>
        [Key]
        public int Id { get; set; }
        #endregion

        #region Información básica
        /// <summary>
        /// Título breve de la incidencia.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Descripción detallada de la incidencia.
        /// </summary>
        [Required]
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Categoría a la que pertenece la incidencia (ej.: "Alumbrado", "Baches").
        /// </summary>
        [Required]
        [MaxLength(60)]
        public string Categoria { get; set; } = "General";
        #endregion

        #region Dirección
        /// <summary>
        /// Dirección estructurada de la incidencia.
        /// </summary>
        [Required]
        public Address Direccion { get; set; } = new();
        #endregion

        #region Estado y metadatos
        /// <summary>
        /// Estado actual de la incidencia.
        /// </summary>
        [Required]
        public EstadoIncidencia Estado { get; set; } = EstadoIncidencia.Pendiente;

        /// <summary>
        /// URL opcional de una foto que ilustre la incidencia.
        /// </summary>
        [MaxLength(500)]
        public string? ImagenUrl { get; set; }

        /// <summary>
        /// Fecha y hora en que se creó la incidencia (UTC).
        /// </summary>
        public DateTime FechaReporte { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora de la última actualización (UTC), si la hubiera.
        /// </summary>
        public DateTime? FechaActualizacion { get; set; }

        /// <summary>
        /// Indica si la incidencia fue eliminada lógicamente.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Fecha y hora en que se realizó el borrado lógico.
        /// </summary>
        public DateTime? FechaEliminacion { get; set; }
        #endregion

        #region Relaciones
        /// <summary>
        /// Identificador del usuario que reportó la incidencia.
        /// </summary>
        [Required]
        public int UsuarioId { get; set; }

        /// <summary>
        /// Navegación al usuario propietario.
        /// </summary>
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }
        #endregion
    }

    /// <summary>
    /// Representa la dirección de una incidencia. Se modela como entidad "owned" de EF Core.
    /// </summary>
    [Owned]
    public class Address
    {
        /// <summary>
        /// Nombre de la calle o avenida.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Calle { get; set; } = string.Empty;

        /// <summary>
        /// Número de altura (puede ser "S/N").
        /// </summary>
        [MaxLength(20)]
        public string Altura { get; set; } = string.Empty;

        /// <summary>
        /// Calles de referencia entre las cuales se ubica la incidencia.
        /// </summary>
        [MaxLength(150)]
        public string? EntreCalles { get; set; }

        /// <summary>
        /// Localidad o barrio dentro de la provincia de Buenos Aires.
        /// </summary>
        [MaxLength(100)]
        public string? Localidad { get; set; } = "Morón";
    }

    /// <summary>
    /// Estados posibles de una incidencia.
    /// </summary>
    public enum EstadoIncidencia
    {
        Pendiente,
        EnProceso,
        Resuelto,
        Rechazado
    }
}
