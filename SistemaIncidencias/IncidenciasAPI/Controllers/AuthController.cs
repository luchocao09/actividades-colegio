using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using IncidenciasAPI.Data;
using IncidenciasAPI.DTOs;
using IncidenciasAPI.Models;

namespace IncidenciasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar si el email ya existe
            bool existe = await _context.Usuarios.AnyAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (existe)
                return BadRequest(new { mensaje = "El correo electrónico ya está registrado." });

            Usuario usuario = new Usuario
            {
                NombreCompleto = dto.NombreCompleto,
                Email = dto.Email.ToLower().Trim(),
                PasswordHash = HashPassword(dto.Password),
                Rol = string.IsNullOrWhiteSpace(dto.Rol) ? "Ciudadano" : dto.Rol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            string token = GenerarJwtToken(usuario);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Id = usuario.IdUsuario,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                Rol = usuario.Rol
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower().Trim());
            if (usuario == null)
                return Unauthorized(new { mensaje = "Credenciales inválidas." });

            if (!VerificarPassword(dto.Password, usuario.PasswordHash))
                return Unauthorized(new { mensaje = "Credenciales inválidas." });

            string token = GenerarJwtToken(usuario);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Id = usuario.IdUsuario,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                Rol = usuario.Rol
            });
        }

        private string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerificarPassword(string password, string hashGuardado)
        {
            string hashCalculado = HashPassword(password);
            return hashCalculado == hashGuardado;
        }

        private string GenerarJwtToken(Usuario usuario)
        {
            string jwtKey = _configuration["Jwt:Key"] ?? "ClavePorDefectoDebeSerMuyLargaParaSeguridad123456!";
            string? jwtIssuer = _configuration["Jwt:Issuer"];
            string? jwtAudience = _configuration["Jwt:Audience"];

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
