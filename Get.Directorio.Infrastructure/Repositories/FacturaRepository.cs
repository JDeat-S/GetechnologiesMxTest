using Get.Directorio.Core.Entities;
using Get.Directorio.Core.Interfaces;
using Get.Directorio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Get.Directorio.Infrastructure.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly DirectorioDbContext _ctx;

        public FacturaRepository(DirectorioDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Factura> AddAsync(Factura entity)
        {
            _ctx.Facturas.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Factura>> GetAllAsync()
        {
            return await _ctx.Facturas.ToListAsync();
        }

        public async Task<Factura?> GetByIdAsync(int id)
        {
            return await _ctx.Facturas.FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task UpdateAsync(Factura factura)
        {
            _ctx.Facturas.Update(factura);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Factura factura)
        {
            _ctx.Facturas.Remove(factura);
            await _ctx.SaveChangesAsync();
        }

    }
}
