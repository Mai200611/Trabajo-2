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

        [Display(Name = "Tipo de Mantenimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(70)(, ErrorMessage = "El campo {0} no puede exceder los 70 caracteres.")]
        public string Tipo_Mantenimiento { get; set; }
        public string Taller { get; set; }
        public decimal Costo { get; set; }
        public string Detalles { get; set; }
        public DateTime Proximo_Mantenimiento { get; set; }

    }
}
