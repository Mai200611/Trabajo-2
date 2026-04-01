using System;
using System.Collections.Generic;
using System.Text;

namespace AutoFleet.Shared.Entities
{
    internal class Vehiculo
    {
        public int Id { get; set; }

        public string Placa { get; set; }

        public string TipoVehiculo { get; set; }

        public string Marca { get; set; }

        public DateTime Año { get; set; }

        public float CapacidadCargaKg { get; set; }

        public string EstadoOperativo { get; set; }

        public float KilometrajeActual { get; set; }
    }
}
