using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace AutoFleet.Shared.Entities
{
    public class Vehiculo
    {
        public int Id { get; set; }

        [Display(Name = "Placa")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Placa { get; set; }


        [Display(Name = "Tipo de Vehiculo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string TipoVehiculo { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Marca { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Modelo { get; set; }

        [Display(Name = "Año")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public int Año { get; set; }

        [Display(Name = "Capacidad de Carga (kg)")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo {0} debe estar entre {1} y {2} kg.")]
        public float CapacidadCargaKg { get; set; }

        [Display(Name = "Estado Operativo")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string EstadoOperativo { get; set; } = "Disponible"; //default como 'Disponible'

        [Display(Name = "Kilometraje Actual")]
        [Range(0, 1000000, ErrorMessage = "El campo {0} debe estar entre {1} y {2} km.")] //Normalmente el contador/odometro llega hasta 1 millon
        public float KilometrajeActual { get; set; }
        public bool IsDeleted { get; set; } = false; // Para eliminación lógica 


        // Relaciones

        [JsonIgnore]
        public ICollection<Recorrido> Recorridos { get; set; } = new List<Recorrido>();
        [JsonIgnore]
        public ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
        [JsonIgnore]
        public ICollection<CargaCombustible> CargasCombustible { get; set; } = new List<CargaCombustible>();
        //Inicializados para que no pida foraenas en el json.

    }
}
