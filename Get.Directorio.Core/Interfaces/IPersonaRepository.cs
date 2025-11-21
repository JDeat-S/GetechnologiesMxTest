using Get.Directorio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Get.Directorio.Core.Interfaces
{
    public interface IPersonaRepository : IRepository<Persona>
    {
        Task<Persona?> GetByIdentificacionAsync(string identificacion);
        Task<List<Factura>> GetFacturasByPersonaIdAsync(int personaId);

    }

}
