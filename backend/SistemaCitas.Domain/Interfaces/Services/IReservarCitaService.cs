using SistemaCitas.Domain.Entities;
using SistemaCitas.Domain.Result_Pattern;

namespace SistemaCitas.Domain.Interfaces.Services
{
    public interface IReservarCitaService
    {
        Task<Result<Cita>> ReservarCitaAsync(Cita cita);
    }
}

