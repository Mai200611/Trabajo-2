using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFleet.Shared.Entities
{
    public class Ruta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código de la ruta")]
        public int Id { get; set; }

        [Display(Name = "Nombre de la ruta")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción de la ruta")]
        [MaxLength(250, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Descripcion { get; set; }

        [Display(Name = "Origen de la ruta")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Origen { get; set; }

        [Display(Name = "Destino de la ruta")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Destino { get; set; }

        [Display(Name = "Distancia (km)")]
        [Range(0, double.MaxValue, ErrorMessage = "La distancia debe ser positiva.")]
        public double Distancia { get; set; }

        [Display(Name = "Duración")]
        public TimeSpan Duracion { get; set; }

        [Display(Name = "Tipo de servicio")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string TipoServicio { get; set; }
    }
}

