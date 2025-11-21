namespace Get.Directorio.Api.DTOs
{
    public class FacturaResponseDto
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string? Concepto { get; set; }
    }

}
