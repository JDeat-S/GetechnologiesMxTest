using Get.Directorio.Core.Entities;
using Get.Directorio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Get.Directorio.Core.Services
{
    public class DirectorioService
    {
        private readonly IPersonaRepository _personaRepo;
        public DirectorioService(IPersonaRepository personaRepo) { _personaRepo = personaRepo; }

        public Task<Persona?> GetByIdAsync(int id) => _personaRepo.GetByIdAsync(id);
        public Task<IEnumerable<Persona>> GetAllAsync() => _personaRepo.GetAllAsync();
        public async Task<Persona> CreateAsync(Persona p)
        {
            // Validaciones adicionales aquí si quieres
            return await _personaRepo.AddAsync(p);
        }
        public Task UpdateAsync(Persona p) => _personaRepo.UpdateAsync(p);
        public Task DeleteAsync(Persona p) => _personaRepo.DeleteAsync(p); // cascada eliminará facturas
    }

}
