using Microsoft.EntityFrameworkCore;
using IncidenciasAPI.Models;

namespace IncidenciasAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ── DbSets ────────────────────────────────────────────────────────────
        public DbSet<Usuario>         Usuarios         { get; set; } = null!;
        public DbSet<Incidente>       Incidentes       { get; set; } = null!;
        public DbSet<Ciudadano>       Ciudadanos       { get; set; } = null!;
        public DbSet<Reporte>         Reportes         { get; set; } = null!;
        public DbSet<UsuarioRepo>     UsuarioRepos     { get; set; } = null!;
        public DbSet<AlertaIncidente> AlertasIncidentes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Índice único para email de Usuario ───────────────────────────
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ── Relación: Ciudadano → Incidente (1 ciudadano : N incidentes) ─
            modelBuilder.Entity<Incidente>()
                .HasOne(i => i.Ciudadano)
                .WithMany(c => c.Incidentes)
                .HasForeignKey(i => i.IdCiudadano)
                .OnDelete(DeleteBehavior.SetNull);

            // ── Relación: Usuario → Incidente (1 usuario : N incidentes) ─────
            modelBuilder.Entity<Incidente>()
                .HasOne(i => i.Usuario)
                .WithMany(u => u.Incidencias)
                .HasForeignKey(i => i.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Relación: Incidente → Reporte (1 incidente : N reportes) ─────
            modelBuilder.Entity<Reporte>()
                .HasOne(r => r.Incidente)
                .WithMany(i => i.Reportes)
                .HasForeignKey(r => r.IdIncidente)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Relación: Incidente → AlertaIncidente (1 : N) ────────────────
            modelBuilder.Entity<AlertaIncidente>()
                .HasOne(a => a.Incidente)
                .WithMany(i => i.Alertas)
                .HasForeignKey(a => a.IdIncidente)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Relación: Usuario → UsuarioRepo (1 usuario : N registros) ────
            modelBuilder.Entity<UsuarioRepo>()
                .HasOne(ur => ur.Usuario)
                .WithMany()
                .HasForeignKey(ur => ur.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Filtro global de borrado lógico (Soft Delete) ─────────────────
            modelBuilder.Entity<Incidente>()
                .HasQueryFilter(i => !i.IsDeleted);
        }
    }
}
