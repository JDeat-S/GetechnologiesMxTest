using Get.Directorio.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Get.Directorio.Infrastructure.Data
{
    public class DirectorioDbContext : DbContext
    {
        public DirectorioDbContext(DbContextOptions<DirectorioDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; } = null!;
        public DbSet<Factura> Facturas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>()
                .HasIndex(p => p.Identificacion)
                .IsUnique();

            modelBuilder.Entity<Persona>()
                .HasMany(p => p.Facturas)
                .WithOne(f => f.Persona)
                .HasForeignKey(f => f.PersonaId)
                .OnDelete(DeleteBehavior.Cascade); // eliminación en cascada
        }
    }
}
