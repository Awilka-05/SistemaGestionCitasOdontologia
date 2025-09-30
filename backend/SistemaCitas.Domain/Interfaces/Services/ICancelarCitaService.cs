using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCitas.Domain.Entities;
using SistemaCitas.Domain.Result_Pattern;

namespace SistemaCitas.Domain.Interfaces.Services
{
    public interface ICancelarCitaService
    {
        Task<Result<Cita>> CancelarCitaAsync(int citaId, Usuario usuario);
    }
}