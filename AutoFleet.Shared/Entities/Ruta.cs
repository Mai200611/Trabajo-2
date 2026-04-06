using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AutoFleet.Shared.Entities
{
    public class Ruta
    {
        [Display(Name = "Codigo de la Ruta")]
        public int Id { get; set; }

        [Display(Name = "Nombre de la Ruta")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción de la Ruta")]
        [MaxLength(250, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string Descripcion { get; set; }

        [Display(Name = "Origen")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string Origen { get; set; }

        [Display(Name = "Destino")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string Destino { get; set; }

        [Display(Name = "Distancia (km)")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor que {1}.")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Distancia { get; set; }

        [Display(Name = "Duración Estimada")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public TimeSpan Duracion { get; set; }

        [Display(Name = "Tipo de Servicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string TipoServicio { get; set; }


        // Relaciones

        [JsonIgnore]
        public ICollection<Recorrido> Recorridos { get; set; } = new List<Recorrido>();

    }
}

