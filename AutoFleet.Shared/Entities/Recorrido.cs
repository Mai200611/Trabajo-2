using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;


namespace AutoFleet.Shared.Entities
{
    public class Recorrido
    {
        [Key]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Código del recorrido")]
        public int CodRecorrido { get; set; }
       

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Fecha del recorrido")]
        public DateTime Fecha { get; set; }


        
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Hora de salida")]
        public TimeOnly HoraSalida { get; set; }
        
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Hora de llegada")]
        public TimeOnly HoraLlegada { get; set; }
        
        [MaxLength(7, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Kilometraje inicial")]
        public int KmInicial { get; set; }
        
        [MaxLength(7, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Kilometraje final")]
        public int KmFinal { get; set; }
        
        [MaxLength(200, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }

        [JsonIgnore]
        public Ruta Ruta { get; set; }
        public int RutaId { get; set; }
        [JsonIgnore]
        public Vehiculo Vehiculo { get; set; }

        public int VehiculoId { get; set; }
        
        [JsonIgnore]
        public Conductor Conductor { get; set; }

        public int ConductorId { get; set; }




    }
}
