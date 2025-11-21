using System.ComponentModel.DataAnnotations;

public class FacturaCreateDto
{
    [Required]
    public int PersonaId { get; set; }
    public decimal Monto { get; set; }
    public string? Concepto { get; set; }
}
