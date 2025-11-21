using Get.Directorio.ClientWPF.Models;

public class UsuarioFacturasDto
{
    public int id { get; set; }
    public string Nombre { get; set; }
    public string ApellidoPaterno { get; set; }
    public string Identificacion { get; set; }
    public string FacturasDescripcion { get; set; }
    public List<FacturaDto> Facturas { get; set; }
}