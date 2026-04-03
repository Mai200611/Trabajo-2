using Microsoft.EntityFrameworkCore;
using AutoFleet.Shared.Entities;

namespace AutoFleet.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<Recorrido> Recorridos { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<Abastecimiento_Combustible> Abastecimientos_Combustible { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ruta>(entity =>
            {
                entity.HasKey(r => r.CodRuta);
                entity.Property(r => r.CodRuta).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Recorrido>(entity =>
            {
                entity.HasKey(r => r.CodRecorrido);
                entity.Property(r => r.CodRecorrido).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}




