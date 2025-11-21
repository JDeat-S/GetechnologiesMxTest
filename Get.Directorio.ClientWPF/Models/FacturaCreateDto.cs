using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Get.Directorio.ClientWPF.Models
{
    public class FacturaCreateDto
    {
        public int PersonaId { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }  
    }
}
