using Get.Directorio.Core.Entities;
using Get.Directorio.Core.Interfaces;

namespace Get.Directorio.Core.Services
{
    public class DirectorioService
    {
        private readonly IPersonaRepository _personaRepo;

        public DirectorioService(IPersonaRepository personaRepo)
        {
            _personaRepo = personaRepo;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            var personas = await _personaRepo.GetAllAsync();

            // Cargar facturas de cada persona
            foreach (var p in personas)
            {
                p.Facturas = await _personaRepo.GetFacturasByPersonaIdAsync(p.Id);
            }

            return personas;
        }

        public async Task<Persona?> GetByIdAsync(int id)
        {
            var persona = await _personaRepo.GetByIdAsync(id);

            if (persona == null)
                return null;

            // Cargar facturas para esa persona
            persona.Facturas = await _personaRepo.GetFacturasByPersonaIdAsync(persona.Id);

            return persona;
        }

        public async Task<Persona> CreateAsync(Persona entity)
        {
            await _personaRepo.AddAsync(entity);
            return entity;
        }


        public async Task DeleteAsync(Persona entity)
        {
            await _personaRepo.DeleteAsync(entity);
        }
    }
}
