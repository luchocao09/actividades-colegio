using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncidenciasAPI.Data;
using IncidenciasAPI.DTOs;
using IncidenciasAPI.Models;

namespace IncidenciasAPI.Controllers
{
    /// <summary>
    /// Controlador para la gestión de incidentes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class IncidenciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IncidenciasController(AppDbContext context)
        {
            _context = context;
        }

        // ── GET: api/incidencias ─────────────────────────────────────────────
        /// <summary>Obtiene todos los incidentes activos (no eliminados).</summary>
        /// <param name="estado">Filtro por estado: Pendiente, EnProceso, Resuelto</param>
        /// <param name="buscar">Texto libre para buscar en descripción o ubicación</param>
        [HttpGet]
        public async Task<IActionResult> GetIncidentes(
            [FromQuery] string? estado,
            [FromQuery] string? buscar)
        {
            IQueryable<Incidente> query = _context.Incidentes
                .Include(i => i.Usuario)
                .Include(i => i.Ciudadano)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(estado))
                query = query.Where(i => i.Estado.ToLower() == estado.ToLower());

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                string termino = buscar.ToLower();
                query = query.Where(i =>
                    i.Descripcion.ToLower().Contains(termino) ||
                    (i.Ubicacion != null && i.Ubicacion.ToLower().Contains(termino)));
            }

            List<IncidenteDetalleDto> incidentes = await query
                .OrderByDescending(i => i.FechaInicio)
                .Select(i => new IncidenteDetalleDto
                {
                    IdIncidente       = i.IdIncidente,
                    Descripcion       = i.Descripcion,
                    Ubicacion         = i.Ubicacion,
                    Estado            = i.Estado,
                    FechaInicio       = i.FechaInicio,
                    FechaFinalizacion = i.FechaFinalizacion,
                    Usuario = i.Usuario == null ? null : new UsuarioResumenDto
                    {
                        IdUsuario      = i.Usuario.IdUsuario,
                        NombreCompleto = i.Usuario.NombreCompleto,
                        Email          = i.Usuario.Email
                    },
                    Ciudadano = i.Ciudadano == null ? null : new CiudadanoResumenDto
                    {
                        IdCiudadano = i.Ciudadano.IdCiudadano,
                        Nombre      = i.Ciudadano.Nombre,
                        Apellido    = i.Ciudadano.Apellido,
                        Dni         = i.Ciudadano.Dni
                    }
                })
                .ToListAsync();

            return Ok(incidentes);
        }

        // ── GET: api/incidencias/5 ───────────────────────────────────────────
        /// <summary>Obtiene un incidente por su ID.</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncidente(int id)
        {
            IncidenteDetalleDto? incidente = await _context.Incidentes
                .Include(i => i.Usuario)
                .Include(i => i.Ciudadano)
                .Where(i => i.IdIncidente == id)
                .Select(i => new IncidenteDetalleDto
                {
                    IdIncidente       = i.IdIncidente,
                    Descripcion       = i.Descripcion,
                    Ubicacion         = i.Ubicacion,
                    Estado            = i.Estado,
                    FechaInicio       = i.FechaInicio,
                    FechaFinalizacion = i.FechaFinalizacion,
                    Usuario = i.Usuario == null ? null : new UsuarioResumenDto
                    {
                        IdUsuario      = i.Usuario.IdUsuario,
                        NombreCompleto = i.Usuario.NombreCompleto,
                        Email          = i.Usuario.Email
                    },
                    Ciudadano = i.Ciudadano == null ? null : new CiudadanoResumenDto
                    {
                        IdCiudadano = i.Ciudadano.IdCiudadano,
                        Nombre      = i.Ciudadano.Nombre,
                        Apellido    = i.Ciudadano.Apellido,
                        Dni         = i.Ciudadano.Dni
                    }
                })
                .FirstOrDefaultAsync();

            if (incidente == null)
                return NotFound(new { mensaje = "Incidente no encontrado." });

            return Ok(incidente);
        }

        // ── POST: api/incidencias ────────────────────────────────────────────
        /// <summary>Crea un nuevo incidente. Requiere autenticación JWT.</summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CrearIncidente([FromBody] CrearIncidenteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
                return Unauthorized(new { mensaje = "Usuario no autenticado correctamente." });

            Incidente incidente = new Incidente
            {
                Descripcion  = dto.Descripcion,
                Ubicacion    = dto.Ubicacion,
                Estado       = "Pendiente",
                FechaInicio  = DateTime.UtcNow,
                IdUsuario    = usuarioId,
                IdCiudadano  = dto.IdCiudadano
            };

            _context.Incidentes.Add(incidente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIncidente), new { id = incidente.IdIncidente }, incidente);
        }

        // ── PATCH: api/incidencias/5/estado ─────────────────────────────────
        /// <summary>Actualiza solo el estado de un incidente. Requiere autenticación JWT.</summary>
        [Authorize]
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] ActualizarEstadoDto dto)
        {
            Incidente? incidente = await _context.Incidentes.FindAsync(id);
            if (incidente == null)
                return NotFound(new { mensaje = "Incidente no encontrado." });

            string[] estadosValidos = { "Pendiente", "EnProceso", "Resuelto" };
            if (!estadosValidos.Contains(dto.Estado, StringComparer.OrdinalIgnoreCase))
                return BadRequest(new { mensaje = "Estado inválido. Valores permitidos: Pendiente, EnProceso, Resuelto." });

            incidente.Estado = dto.Estado;
            if (dto.Estado.Equals("Resuelto", StringComparison.OrdinalIgnoreCase))
                incidente.FechaFinalizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Estado actualizado.", estado = incidente.Estado });
        }

        // ── PUT: api/incidencias/5 ───────────────────────────────────────────
        /// <summary>Actualiza los datos de un incidente. Requiere autenticación JWT.</summary>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarIncidente(int id, [FromBody] ActualizarIncidenteDto dto)
        {
            Incidente? incidente = await _context.Incidentes.FindAsync(id);
            if (incidente == null)
                return NotFound(new { mensaje = "Incidente no encontrado." });

            string? usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? rolClaim       = User.FindFirst(ClaimTypes.Role)?.Value;

            if (usuarioIdClaim != null && int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                if (incidente.IdUsuario != usuarioId && rolClaim != "Administrador")
                    return Forbid();
            }

            if (!string.IsNullOrWhiteSpace(dto.Descripcion)) incidente.Descripcion = dto.Descripcion;
            if (!string.IsNullOrWhiteSpace(dto.Ubicacion))   incidente.Ubicacion   = dto.Ubicacion;
            if (!string.IsNullOrWhiteSpace(dto.Estado))      incidente.Estado      = dto.Estado;

            await _context.SaveChangesAsync();
            return Ok(incidente);
        }

        // ── DELETE: api/incidencias/5 ────────────────────────────────────────
        /// <summary>Elimina lógicamente un incidente (soft delete). Requiere autenticación JWT.</summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarIncidente(int id)
        {
            Incidente? incidente = await _context.Incidentes.FindAsync(id);
            if (incidente == null)
                return NotFound(new { mensaje = "Incidente no encontrado." });

            string? usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? rolClaim       = User.FindFirst(ClaimTypes.Role)?.Value;

            if (usuarioIdClaim != null && int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                if (incidente.IdUsuario != usuarioId && rolClaim != "Administrador")
                    return Forbid();
            }

            incidente.IsDeleted       = true;
            incidente.FechaFinalizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Incidente eliminado lógicamente con éxito." });
        }
    }
}
