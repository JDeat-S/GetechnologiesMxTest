using Get.Directorio.Core.Entities;
using Get.Directorio.Core.Interfaces;

namespace Get.Directorio.Core.Services
{
    public class VentasService
    {
        private readonly IFacturaRepository _facturaRepo;

        public VentasService(IFacturaRepository facturaRepo)
        {
            _facturaRepo = facturaRepo;
        }

        public Task<IEnumerable<Factura>> GetAllAsync() => _facturaRepo.GetAllAsync();

        public Task<Factura?> GetByIdAsync(int id) => _facturaRepo.GetByIdAsync(id);

        public Task<Factura> CreateAsync(Factura factura) => _facturaRepo.AddAsync(factura);
    }
}
