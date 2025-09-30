using Mapster;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionCitas.Application.DTOs.Requests;
using SistemaGestionCitas.Application.DTOs.Responses;
using SistemaGestionCitas.Domain.Entities;
using SistemaGestionCitas.Domain.Interfaces.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaGestionCitas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorService lugarService, ILogger<DoctorController> logger)
        {
            _doctorService = lugarService;
            _logger = logger;
        }

        // GET: api/<LugarController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorResponseDto>>> GetAll()
        {
            var result = await _doctorService.GetAllAsync();

            if (result.IsFailure)
            {
                ModelState.AddModelError("Error", result.Error);
                return BadRequest(ModelState);
            }

            var response = result.Value.Adapt<IEnumerable<DoctorResponseDto>>();
            return Ok(response);
        }

        // GET api/<LugarController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorResponseDto>> GetById(short id)
        {
            var result = await _doctorService.GetByIdAsync(id);

            if (result.IsFailure)
            {
                ModelState.AddModelError("Error", result.Error);
                return NotFound(ModelState);
            }

            var response = result.Value.Adapt<DoctorResponseDto>();
            return Ok(response);
        }

        // POST api/<LugarController>
        [HttpPost]
        public async Task<ActionResult<DoctorResponseDto>> Add([FromBody] DoctorDto dto)
        {
            var lugar = dto.Adapt<Doctor>();

            var result = await _doctorService.AddAsync(lugar);

            if (result.IsFailure)
            {
                ModelState.AddModelError("Error", result.Error);
                return BadRequest(ModelState);
            }
            var response = result.Value.Adapt<DoctorResponseDto>();

            return Ok(response);
        }


        // PUT api/<LugarController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorResponseDto>> Update(short id, [FromBody] DoctorDto dto)
        {
            var lugar = dto.Adapt<Doctor>();
            lugar.DoctorId = id;

            var result = await _doctorService.UpdateAsync(lugar);

            if (result.IsFailure)
            {
                ModelState.AddModelError("Error", result.Error);
                return BadRequest(ModelState);
            }

            var response = result.Value.Adapt<DoctorResponseDto>();
            return Ok(response);
        }
    }
}
