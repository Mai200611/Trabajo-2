using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoFleet.Shared.Entities
{
    internal class Mantenimientos
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo_Mantenimiento { get; set; }
        public string Taller { get; set; }
        public decimal Costo { get; set; }
        public string Detalles { get; set; }
        public DateTime Proximo_Mantenimiento { get; set; }

    }
}
