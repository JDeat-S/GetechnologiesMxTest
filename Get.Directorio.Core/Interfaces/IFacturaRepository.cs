using Get.Directorio.Core.Entities;

namespace Get.Directorio.Core.Interfaces
{
    public interface IFacturaRepository
    {
        Task<Factura> AddAsync(Factura entity);
        Task<IEnumerable<Factura>> GetAllAsync();
        Task<Factura?> GetByIdAsync(int id);
    }
}
