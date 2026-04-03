using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoFleet.Shared.Entities
{
    public class CargaCombustible
    {

        public int Id { get; set; }

        [Display(Name = "Fecha de Carga/Tanqueo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Cantidad de Combustible (L)")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.01, 1200.0, ErrorMessage = "La {0} debe Estar entre {1} Litros y {2}.")]
        public decimal CantidadCombustible { get; set; }

        [Display(Name = "Costo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El {0} debe estar entre {1} y {2}.")] //de 0 a valor maximo para que no agarre valores negativos
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CostoTotal { get; set; }

        [Display(Name = "Kilometraje (Km)")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.0, 1500000.0, ErrorMessage = "El campo de {0} debe estar entre {1} y {2}.")]
        public int Kilometraje { get; set; }

        [Display(Name = "Gasolinera")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(80, ErrorMessage = "El campo de {0} no puede superar los {1} caracteres.")]
        public string Gasolinera { get; set; }


        // Relaciones

        [JsonIgnore]
        public Vehiculo Vehiculo { get; set; }

        [Display(Name = "Id Vehiculo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int VehiculoId { get; set; }
    }
}
