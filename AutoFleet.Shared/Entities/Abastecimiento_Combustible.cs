using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFleet.Shared.Entities
{
    public class Abastecimiento_Combustible
    {
        
        [Display(Name = "Vehiculo")]
        [Required(ErrorMessage = "El campo Vehiculo es obligatorio.")]
        public int Id_Vehiculo { get; set; }
        [ForeignKey("Id_Vehiculo")] //FK de Vehiculo
        public Vehiculo? Vehiculo { get; set; }

        [DataType(DataType.Date)] //Fecha 
        [Column(TypeName = "date")]
        [Display(Name = "Fecha de Uso")]
        public DateTime? Fecha { get; set; }

        [Display(Name = "Cantidad de Combustible (L)")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.01, 100.0, ErrorMessage = "La cantidad de combustible debe Estar entre 0.01L y 100L (Litros).")]
        public double Cantidad_Combustible { get; set; }  //Cantidad de combustible en litros
         
        [Display(Name = "Costo (COP)")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(5000, 500000.0, ErrorMessage = "El costo total debe estar entre 5000 y 500000 (COP).")] //Precio Maximo de 500.000 COP para evitar errores de digitación
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Costo_Total { get; set; } //Costo total  abastecimiento (COP)

        [Display(Name = "Kilometraje (Km)")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.0, 1500000.0, ErrorMessage = "El kilometraje debe estar entre 0.0 y 1,500,000 km.")]
        public double Kilometraje { get; set; } //Kilometraje del vehículo al momento del abastecimiento

        [Display(Name = "Gasolinera")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(12, ErrorMessage = "El NIT de la gasolinera no puede exceder los 12 caracteres.")]
        public string Gasolinera { get; set; } //NIT de la gasolinera donde se realizó el abastecimiento
    }
}
