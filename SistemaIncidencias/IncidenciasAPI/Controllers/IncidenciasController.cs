using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IncidenciasAPI.Data;
using IncidenciasAPI.DTOs;
using IncidenciasAPI.Models;

namespace IncidenciasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidenciasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IncidenciasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/incidencias
        [HttpGet]
        public async Task<IActionResult> GetIncidencias(
            [FromQuery] string? estado,
            [FromQuery] string? categoria,
            [FromQuery] string? buscar)
        {
            IQueryable<Incidencia> query = _context.Incidencias
                .Include(i => i.Usuario)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(estado) && Enum.TryParse<EstadoIncidencia>(estado, true, out EstadoIncidencia estadoEnum))
            {
                query = query.Where(i => i.Estado == estadoEnum);
            }

            if (!string.IsNullOrWhiteSpace(categoria))
            {
                query = query.Where(i => i.Categoria.ToLower() == categoria.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                string termino = buscar.ToLower();
                query = query.Where(i => i.Titulo.ToLower().Contains(termino) || 
                                         i.Descripcion.ToLower().Contains(termino) ||
                                         i.Direccion.Calle.ToLower().Contains(termino) ||
                                         (i.Direccion.EntreCalles != null && i.Direccion.EntreCalles.ToLower().Contains(termino)) ||
                                         (i.Direccion.Localidad != null && i.Direccion.Localidad.ToLower().Contains(termino)));
            }

            List<IncidenciaDetalleDto> incidencias = await query
                .OrderByDescending(i => i.FechaReporte)
                .Select(i => new IncidenciaDetalleDto
                {
                    Id = i.Id,
                    Titulo = i.Titulo,
                    Descripcion = i.Descripcion,
                    Categoria = i.Categoria,
                    Calle = i.Direccion.Calle,
                    Altura = i.Direccion.Altura,
                    EntreCalles = i.Direccion.EntreCalles,
                    Localidad = i.Direccion.Localidad,
                    Estado = i.Estado.ToString(),
                    ImagenUrl = i.ImagenUrl,
                    FechaReporte = i.FechaReporte,
                    FechaActualizacion = i.FechaActualizacion,
                    Usuario = i.Usuario == null ? null : new UsuarioResumenDto
                    {
                        Id = i.Usuario.Id,
                        NombreCompleto = i.Usuario.NombreCompleto,
                        Email = i.Usuario.Email
                    }
                })
                .ToListAsync();

            return Ok(incidencias);
        }

        // GET: api/incidencias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncidencia(int id)
        {
            IncidenciaDetalleDto? incidencia = await _context.Incidencias
                .Include(i => i.Usuario)
                .Where(i => i.Id == id)
                .Select(i => new IncidenciaDetalleDto
                {
                    Id = i.Id,
                    Titulo = i.Titulo,
                    Descripcion = i.Descripcion,
                    Categoria = i.Categoria,
                    Calle = i.Direccion.Calle,
                    Altura = i.Direccion.Altura,
                    EntreCalles = i.Direccion.EntreCalles,
                    Localidad = i.Direccion.Localidad,
                    Estado = i.Estado.ToString(),
                    ImagenUrl = i.ImagenUrl,
                    FechaReporte = i.FechaReporte,
                    FechaActualizacion = i.FechaActualizacion,
                    Usuario = i.Usuario == null ? null : new UsuarioResumenDto
                    {
                        Id = i.Usuario.Id,
                        NombreCompleto = i.Usuario.NombreCompleto,
                        Email = i.Usuario.Email
                    }
                })
                .FirstOrDefaultAsync();

            if (incidencia == null)
                return NotFound(new { mensaje = "Incidencia no encontrada." });

            return Ok(incidencia);
        }

        // POST: api/incidencias
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CrearIncidencia([FromBody] CrearIncidenciaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return Unauthorized(new { mensaje = "Usuario no autenticado correctamente." });
            }

            Incidencia incidencia = new Incidencia
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Categoria = dto.Categoria,
                Direccion = new Address
                {
                    Calle = dto.Calle,
                    Altura = dto.Altura,
                    EntreCalles = dto.EntreCalles,
                    Localidad = string.IsNullOrWhiteSpace(dto.Localidad) ? "Morón" : dto.Localidad
                },
                ImagenUrl = dto.ImagenUrl,
                Estado = EstadoIncidencia.Pendiente,
                FechaReporte = DateTime.UtcNow,
                UsuarioId = usuarioId
            };

            _context.Incidencias.Add(incidencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIncidencia), new { id = incidencia.Id }, incidencia);
        }

        // PATCH: api/incidencias/5/estado
        [Authorize]
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] ActualizarEstadoDto dto)
        {
            Incidencia? incidencia = await _context.Incidencias.FindAsync(id);
            if (incidencia == null)
                return NotFound(new { mensaje = "Incidencia no encontrada." });

            if (!Enum.TryParse<EstadoIncidencia>(dto.Estado, true, out EstadoIncidencia nuevoEstado))
            {
                return BadRequest(new { mensaje = "Estado inválido. Valores permitidos: Pendiente, EnProceso, Resuelto, Rechazado." });
            }

            incidencia.Estado = nuevoEstado;
            incidencia.FechaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Estado actualizado exitosamente.", estado = incidencia.Estado.ToString() });
        }

        // PUT: api/incidencias/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarIncidencia(int id, [FromBody] ActualizarIncidenciaDto dto)
        {
            Incidencia? incidencia = await _context.Incidencias.FindAsync(id);
            if (incidencia == null)
                return NotFound(new { mensaje = "Incidencia no encontrada." });

            string? usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? rolClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (usuarioIdClaim != null && int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                // Solo el creador o un Administrador puede editar la incidencia
                if (incidencia.UsuarioId != usuarioId && rolClaim != "Administrador")
                {
                    return Forbid();
                }
            }

            if (!string.IsNullOrWhiteSpace(dto.Titulo)) incidencia.Titulo = dto.Titulo;
            if (!string.IsNullOrWhiteSpace(dto.Descripcion)) incidencia.Descripcion = dto.Descripcion;
            if (!string.IsNullOrWhiteSpace(dto.Categoria)) incidencia.Categoria = dto.Categoria;
            if (!string.IsNullOrWhiteSpace(dto.Calle)) incidencia.Direccion.Calle = dto.Calle;
            if (!string.IsNullOrWhiteSpace(dto.Altura)) incidencia.Direccion.Altura = dto.Altura;
            if (dto.EntreCalles != null) incidencia.Direccion.EntreCalles = dto.EntreCalles;
            if (dto.Localidad != null) incidencia.Direccion.Localidad = dto.Localidad;
            if (dto.ImagenUrl != null) incidencia.ImagenUrl = dto.ImagenUrl;

            incidencia.FechaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(incidencia);
        }

        // DELETE: api/incidencias/5 (Borrado Lógico)
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarIncidencia(int id)
        {
            Incidencia? incidencia = await _context.Incidencias.FindAsync(id);
            if (incidencia == null)
                return NotFound(new { mensaje = "Incidencia no encontrada." });

            string? usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? rolClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (usuarioIdClaim != null && int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                // Solo el creador o un Administrador puede eliminar la incidencia
                if (incidencia.UsuarioId != usuarioId && rolClaim != "Administrador")
                {
                    return Forbid();
                }
            }

            // Aplicar Borrado Lógico (Soft Delete)
            incidencia.IsDeleted = true;
            incidencia.FechaEliminacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Incidencia eliminada lógicamente con éxito." });
        }
    }
}
