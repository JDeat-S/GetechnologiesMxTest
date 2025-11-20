using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Get.Directorio.Core.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        [Required] public int PersonaId { get; set; }
        [ForeignKey(nameof(PersonaId))] public Persona Persona { get; set; } = null!;
        [Required] public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string? Concepto { get; set; }
    }
}
