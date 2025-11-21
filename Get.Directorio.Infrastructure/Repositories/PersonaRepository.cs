using Get.Directorio.Core.Entities;
using Get.Directorio.Core.Interfaces;
using Get.Directorio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Get.Directorio.Infrastructure.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly DirectorioDbContext _ctx;
        public PersonaRepository(DirectorioDbContext ctx) => _ctx = ctx;

        public async Task<Persona> AddAsync(Persona entity)
        {
            _ctx.Personas.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
        public async Task<List<Factura>> GetFacturasByPersonaIdAsync(int personaId)
        {
            return await _ctx.Facturas
                .Where(f => f.PersonaId == personaId)
                .ToListAsync();
        }


        public async Task DeleteAsync(Persona entity)
        {
            _ctx.Personas.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
            => await _ctx.Personas.Include(p => p.Facturas).ToListAsync();

        public async Task<Persona?> GetByIdAsync(int id)
            => await _ctx.Personas.Include(p => p.Facturas).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Persona?> GetByIdentificacionAsync(string identificacion)
            => await _ctx.Personas.Include(p => p.Facturas)
                    .FirstOrDefaultAsync(p => p.Identificacion == identificacion);

        public async Task UpdateAsync(Persona entity)
        {
            _ctx.Personas.Update(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
