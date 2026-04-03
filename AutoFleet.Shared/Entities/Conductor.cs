using System.ComponentModel.DataAnnotations;

namespace AutoFleet.Shared.Entities
{
    public class Conductor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El documento es obligatorio")]
        [MaxLength(20)]
        public string Documento { get; set; } = null!;

        [Required(ErrorMessage = "La licencia es obligatoria")]
        [MaxLength(20)]
        public string Licencia { get; set; } = null!;

        [Required]
        public string Categoria { get; set; } = null!;

        [Required]
        public DateTime FechaVencimiento { get; set; }

        [MaxLength(15)]
        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public bool EstadoDisponibilidad { get; set; }

        public ICollection<Recorrido>? Recorridos { get; set; }
    }
}