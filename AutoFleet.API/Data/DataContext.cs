using Microsoft.EntityFrameworkCore;
using AutoFleet.Shared.Entities;

namespace AutoFleet.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Abastecimiento_Combustible> Abastecimientos { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<Recorrido> Recorridos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ruta>(entity =>
            {
                entity.HasKey(r => r.CodRuta);
                entity.Property(r => r.CodRuta).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
