using Mapster;
using Microsoft.AspNetCore.Mvc;
using SistemaCitas.Application.DTOs.Requests;
using SistemaCitas.Application.DTOs.Responses;
using SistemaCitas.Application.Services;
using SistemaCitas.Domain.Entities;
using SistemaCitas.Domain.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaCitas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionTurnoController : ControllerBase
    {

        private readonly IConfiguracionTurnoService _configuracionTurnoService;

        public ConfiguracionTurnoController(IConfiguracionTurnoService configuracionTurnoService)
        {
            _configuracionTurnoService = configuracionTurnoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _configuracionTurnoService.GetAllAsync();
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _configuracionTurnoService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpPost]
        public async Task<ActionResult<ConfiguracionTurnoResponseDto>> Add([FromBody] ConfiguracionTurnoDto dto)
        {
            var turno = dto.Adapt<ConfiguracionTurno>();

            var result = await _configuracionTurnoService.AddAsync(turno);

            if (result.IsFailure)
            {
                ModelState.AddModelError("Error", result.Error);
                return BadRequest(ModelState);
            }
            var response = result.Value.Adapt<ConfiguracionTurnoResponseDto>();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConfiguracionTurnoResponseDto>> Update(int id, [FromBody] ConfiguracionTurnoDto dto)
        {
            var turno = dto.Adapt<ConfiguracionTurno>();
            turno.TurnoId = id;

            var result = await _configuracionTurnoService.UpdateAsync(turno);

            if (result.IsFailure)
            {
                ModelState.AddModelError("Error", result.Error);
                return BadRequest(ModelState);
            }

            var response = result.Value.Adapt<ConfiguracionTurnoResponseDto>();
            return Ok(response);
        }

    }
}
