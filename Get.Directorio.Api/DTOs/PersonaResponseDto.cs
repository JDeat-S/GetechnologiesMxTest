namespace Get.Directorio.Api.DTOs
{
    public class PersonaResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Identificacion { get; set; }
        public List<FacturaResponseDto> Facturas { get; set; } = new();

    }

}
