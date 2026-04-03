using System.ComponentModel.DataAnnotations;

namespace AutoFleet.Shared.Entities
{
    public class Mantenimiento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El tipo de mantenimiento es obligatorio")]
        [MaxLength(100)]
        public string Tipo { get; set; } = null!;

        [Required(ErrorMessage = "La entidad responsable es obligatoria")]
        [MaxLength(200)]
        public string EntidadResponsable { get; set; } = null!;

        [Required(ErrorMessage = "El costo es obligatorio")]
        public decimal Costo { get; set; }

        [MaxLength(500)]
        public string? Detalles { get; set; }

        public DateTime? ProximoMantenimiento { get; set; }

        // Relaciones

        [Required]
        public int VehiculoId { get; set; }

        public Vehiculo? Vehiculo { get; set; }
    }
}
