using SistemaCitas.Domain.Entities;
using SistemaCitas.Domain.Enums;
using SistemaCitas.Domain.Result_Pattern;

namespace SistemaCitas.Domain.Interfaces.Services
{
    public interface ICitaService
    {
        Task<Result<Cita>> GetByIdAsync(int id);
        Task<Result<IEnumerable<Cita>>> GetAllAsync();
        Task<Result<IEnumerable<Cita>>> GetByFechaAsync(DateTime fecha);
        Task<Result<IEnumerable<Cita>>> GetByUsuarioAsync(int usuarioId);
        Task<IEnumerable<Cita>> GetByEstadoAsync(EstadoCita estado);

    }
}