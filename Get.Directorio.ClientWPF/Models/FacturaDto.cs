using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Get.Directorio.ClientWPF.Models
{
    public class FacturaDto
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; }
        public DateTime Fecha { get; set; }
    }

}
