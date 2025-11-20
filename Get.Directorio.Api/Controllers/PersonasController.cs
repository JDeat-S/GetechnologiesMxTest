using Get.Directorio.Core.Entities;
using Get.Directorio.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Get.Directorio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly DirectorioService _service;
        public PersonasController(DirectorioService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _service.GetByIdAsync(id);
            return p == null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonaCreateDto dto)
        {
            var persona = new Persona
            {
                Nombre = dto.Nombre,
                ApellidoPaterno = dto.ApellidoPaterno,
                ApellidoMaterno = dto.ApellidoMaterno,
                Identificacion = dto.Identificacion,
            };

            var created = await _service.CreateAsync(persona);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _service.GetByIdAsync(id);
            if (p == null) return NotFound();
            await _service.DeleteAsync(p);
            return NoContent();
        }
    }
}
