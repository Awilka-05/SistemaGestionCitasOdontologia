using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCitas.Domain.Entities;
using SistemaCitas.Domain.Result_Pattern;

namespace SistemaCitas.Application.Validators
{
    public interface ICitaValidator
    {
        Task<Result<Cita>> ValidarCreacionAsync(Cita entity);
        Task<Result<Cita>> ValidarCancelacionAsync(int citaId, Usuario usuario);
    }
}
