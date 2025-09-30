using Microsoft.Extensions.Logging;
using SistemaGestionCitas.Domain.Entities;
using SistemaGestionCitas.Domain.Interfaces.Repositories;
using SistemaGestionCitas.Domain.Interfaces.Services;
using SistemaGestionCitas.Domain.Result_Pattern;

namespace SistemaGestionCitas.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<Doctor,short> _repository;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(IRepository<Doctor,short> repository, ILogger<DoctorService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<Doctor>> GetByIdAsync(short id)
        {
            var lugar = await _repository.GetByIdAsync(id);
            if (lugar == null)
            {
                _logger.LogWarning($"Lugar con ID {id} no encontrado.");
                return Result<Doctor>.Failure("Lugar no encontrado.");
            }

            return Result<Doctor>.Success(lugar);
        }

        public async Task<Result<IEnumerable<Doctor>>> GetAllAsync()
        {
            var lugares = await _repository.GetAllAsync();
            return Result<IEnumerable<Doctor>>.Success(lugares);
        }

        public async Task<Result<Doctor>> AddAsync(Doctor entity)
        {
            var all = await _repository.GetAllAsync();
            if (all.Any(l => l.Nombre.ToLower() == entity.Nombre.ToLower()))
            {
                _logger.LogWarning($"Ya existe un lugar con el nombre '{entity.Nombre}'.");
                return Result<Doctor>.Failure("El lugar ya existe.");
            }

            await _repository.AddAsync(entity);
            _logger.LogInformation($"Lugar '{entity.Nombre}' agregado exitosamente.");
            return Result<Doctor>.Success(entity);
        }

        public async Task<Result<Doctor>> UpdateAsync(Doctor entity)
        {
            var lugar = await _repository.GetByIdAsync(entity.DoctorId);
            if (lugar == null)
            {
                _logger.LogWarning($"Lugar con ID {entity.DoctorId} no encontrado para actualizar.");
                return Result<Doctor>.Failure("Lugar no encontrado.");
            }

            await _repository.UpdateAsync(entity);
            _logger.LogInformation($"Lugar '{entity.Nombre}' actualizado.");
            return Result<Doctor>.Success(entity);
        }
    }

}
