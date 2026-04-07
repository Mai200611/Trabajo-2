using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;


namespace AutoFleet.Shared.Entities
{
    public class Recorrido
    {

        [Display(Name = "Código del recorrido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Id { get; set; }

        [Display(Name = "Fecha del recorrido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Hora de salida")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public TimeOnly HoraSalida { get; set; }

        [Display(Name = "Hora de llegada")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public TimeOnly HoraLlegada { get; set; }

        [Display(Name = "Kilometraje inicial")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int KmInicial { get; set; }


        [Display(Name = "Kilometraje final")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int KmFinal { get; set; }

        [Display(Name = "Observaciones")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Observaciones { get; set; } //Opcional


        // Relaciones

        [JsonIgnore]
        public Ruta? Ruta { get; set; }

        [Display(Name = "Codigo de Ruta")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int RutaId { get; set; }

        [JsonIgnore]
        public Vehiculo? Vehiculo { get; set; }

        [Display(Name = "Codigo del Vehiculo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int VehiculoId { get; set; }

        [JsonIgnore]
        public Conductor? Conductor { get; set; }

        [Display(Name = "Codigo del Conductor")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ConductorId { get; set; }
    }
}
