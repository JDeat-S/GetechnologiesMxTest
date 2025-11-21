using Get.Directorio.Api.DTOs;
using Get.Directorio.Core.Entities;
using Get.Directorio.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Get.Directorio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly VentasService _service;
        private readonly DirectorioService _personaService;

        public FacturasController(VentasService service, DirectorioService personaService)
        {
            _service = service;
            _personaService = personaService;
        }

        // GET api/facturas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var facturas = await _service.GetAllAsync();

            var list = facturas.Select(f => new FacturaResponseDto
            {
                Id = f.Id,
                PersonaId = f.PersonaId,
                Monto = f.Monto,
                Fecha = f.Fecha,
                Concepto = f.Concepto
            });

            return Ok(list);
        }

        // GET api/facturas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var f = await _service.GetByIdAsync(id);
            if (f == null)
                return NotFound(new { message = $"No existe la factura con ID {id}" });

            var dto = new FacturaResponseDto
            {
                Id = f.Id,
                PersonaId = f.PersonaId,
                Monto = f.Monto,
                Fecha = f.Fecha,
                Concepto = f.Concepto
            };

            return Ok(dto);
        }

        // POST api/facturas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FacturaCreateDto dto)
        {
            // Validación: persona debe existir
            var persona = await _personaService.GetByIdAsync(dto.PersonaId);
            if (persona == null)
            {
                return BadRequest(new
                {
                    message = $"No existe una persona con ID {dto.PersonaId}"
                });
            }

            var factura = new Factura
            {
                PersonaId = dto.PersonaId,
                Monto = dto.Monto,
                Concepto = dto.Concepto,
                Fecha = DateTime.UtcNow
            };

            var created = await _service.CreateAsync(factura);

            var response = new FacturaResponseDto
            {
                Id = created.Id,
                PersonaId = created.PersonaId,
                Monto = created.Monto,
                Fecha = created.Fecha,
                Concepto = created.Concepto
            };

            return Ok(response);
        }
    }
}
