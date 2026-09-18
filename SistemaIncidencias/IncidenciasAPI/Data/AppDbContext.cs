using Microsoft.EntityFrameworkCore;
using IncidenciasAPI.Models;

namespace IncidenciasAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Incidencia> Incidencias { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Índice único para email
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Relación Usuario -> Incidencias
            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.Usuario)
                .WithMany(u => u.Incidencias)
                .HasForeignKey(i => i.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Filtro Global de Borrado Lógico (Soft Delete)
            modelBuilder.Entity<Incidencia>()
                .HasQueryFilter(i => !i.IsDeleted);
        }
    }
}
