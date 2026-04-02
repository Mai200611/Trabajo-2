using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace AutoFleet.Shared.Entities
{
    internal class Mantenimiento
    {
        [Display(Name = "Vehiculo")]
        [Required(ErrorMessage = "El campo Vehiculo es obligatorio.")]
        public int Id { get; set; }
        [ForeignKey("Id_Vehiculo")] //FK de Vehiculo
        public Vehiculo? Vehiculo { get; set; }

        [DataType(DataType.Date)] //Fecha Mantenimiento
        [Column(TypeName = "date")]
        [Display(Name = "Fecha de Mantenimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Tipo de Mantenimiento")] //Tipo de mantenimiento realizado (preventivo, correctivo, etc.)
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(70)(, ErrorMessage = "El campo {0} no puede exceder los 70 caracteres.")]
        public string Tipo_Mantenimiento { get; set; }

        [Display(Name = "Taller")] //NIT del taller donde se realizó el mantenimiento
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(12, ErrorMessage = "El NIT del taller no puede exceder los 12 caracteres.")]
        public string Taller { get; set; }

        [Display(Name = "Costo (COP)")] //Costo total del mantenimiento en pesos (COP)
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Costo { get; set; }

        [Display(Name = "Detalles")] //Detalles adicionales del mantenimiento
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(200)(, ErrorMessage = "El campo {0} no puede exceder los 200 caracteres.")]
        public string Detalles { get; set; }

        [DataType(DataType.Date)] //Fecha siguiente Mantenimiento
        [Column(TypeName = "date")]
        [Display(Name = "Siguiente Mantenimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Proximo_Mantenimiento { get; set; }

    }
}
