using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Get.Directorio.Core.Entities
{
    public class Persona
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string Identificacion { get; set; } = null!;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Relación 1 - N
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }

}
