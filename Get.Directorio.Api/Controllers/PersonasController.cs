using Get.Directorio.Api.DTOs;
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

        public PersonasController(DirectorioService service)
        {
            _service = service;
        }

        // GET api/personas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personas = await _service.GetAllAsync();

            var list = personas.Select(p => new PersonaResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                ApellidoPaterno = p.ApellidoPaterno,
                ApellidoMaterno = p.ApellidoMaterno,
                Identificacion = p.Identificacion,
                Facturas = p.Facturas.Select(f => new FacturaResponseDto
                {
                    Id = f.Id,
                    PersonaId = f.PersonaId,
                    Monto = f.Monto,
                    Fecha = f.Fecha,
                    Concepto = f.Concepto
                }).ToList()
            });

            return Ok(list);
        }

        // GET api/personas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _service.GetByIdAsync(id);
            if (p == null)
                return NotFound(new { message = $"No existe la persona con ID {id}" });

            var dto = new PersonaResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                ApellidoPaterno = p.ApellidoPaterno,
                ApellidoMaterno = p.ApellidoMaterno,
                Identificacion = p.Identificacion,
                Facturas = p.Facturas.Select(f => new FacturaResponseDto
                {
                    Id = f.Id,
                    PersonaId = f.PersonaId,
                    Monto = f.Monto,
                    Fecha = f.Fecha,
                    Concepto = f.Concepto
                }).ToList()
            };

            return Ok(dto);
        }

        // POST api/personas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonaCreateDto dto)
        {
            var persona = new Persona
            {
                Nombre = dto.Nombre,
                ApellidoPaterno = dto.ApellidoPaterno,
                ApellidoMaterno = dto.ApellidoMaterno,
                Identificacion = dto.Identificacion,
                FechaRegistro = DateTime.UtcNow
            };

            var created = await _service.CreateAsync(persona);

            var response = new PersonaResponseDto
            {
                Id = created.Id,
                Nombre = created.Nombre,
                ApellidoPaterno = created.ApellidoPaterno,
                ApellidoMaterno = created.ApellidoMaterno,
                Identificacion = created.Identificacion
            };

            return CreatedAtAction(nameof(Get), new { id = created.Id }, response);
        }

        // DELETE api/personas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _service.GetByIdAsync(id);
            if (p == null)
                return NotFound(new { message = $"No existe la persona con ID {id}" });

            await _service.DeleteAsync(p);
            return NoContent();
        }
    }
}
