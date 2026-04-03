using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AutoFleet.Shared.Entities
{
    public class Conductor
    {
        [Display(Name = "ID del Conductor")]
        public int Id { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string Nombre { get; set; }

        [Display(Name = "Documento de Identidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string Documento { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(15, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string Telefono { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string Direccion { get; set; }

        [Display(Name = "Número de Licencia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string LicenciaNumero { get; set; }

        [Display(Name = "Categoría de Licencia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        public string LicenciaCategoria { get; set; }

        [Display(Name = "Fecha de Vencimiento de Licencia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime LicenciaVencimiento { get; set; }

        [Display(Name = "Estado de Disponibilidad")]
        public bool EstadoDisponibilidad { get; set; } = true;  // True | False --> Disponible | NoDisponible


        //Relaciones

        [JsonIgnore]
        public ICollection<Recorrido> Recorridos { get; set; }
    }
}