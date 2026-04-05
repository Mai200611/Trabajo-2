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
        public DbSet<CargaCombustible> CargasCombustible { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ruta>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Recorrido>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Vehiculo>() //que la placa sea única
                .HasIndex(v => v.Placa)
                .IsUnique();

            modelBuilder.Entity<Conductor>() //que el número de Licencia sea único
                .HasIndex(c => c.Licencia)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}




