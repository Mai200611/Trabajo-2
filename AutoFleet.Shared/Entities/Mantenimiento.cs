using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoFleet.Shared.Entities
{
    public class Mantenimiento
    {

        [Display(Name = "Codigo del Mantenimiento")]
        public int Id { get; set; }

        [Display(Name = "Fecha del Mantenimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Display(Name = "Tipo de Mantenimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string Tipo { get; set; } = string.Empty;

        [Display(Name = "Entidad Responsable")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string EntidadResponsable { get; set; } = string.Empty;

        [Display(Name = "Costo del Mantenimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El {0} debe estar entre {1} y {2}.")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Costo { get; set; }

        [Display(Name = "Detalles Adicionales")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string? Detalles { get; set; }

        [Display(Name = "Próximo Mantenimiento Sugerido")]
        [Column(TypeName = "date")]
        public DateTime? ProximoMantenimiento { get; set; }
        // Nulable porque puede no haber proximo mantenimiento agendado despues del registrado
        // O si se hizo una reparacion menor


        // Relaciones

        [JsonIgnore]
        public Vehiculo? Vehiculo { get; set; }

        [Display(Name = "Codigo del Vehiculo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int VehiculoId { get; set; }
    }
}