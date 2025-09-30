using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCitas.Domain.Entities;

namespace SistemaCitas.Infrastructure.Services.Correo.Strategy
{
    public interface ICorreoEstrategia
    {
        Task EnviarAsync(Cita cita, Usuario usuario, FranjaHorario franjaSeleccionada);
    }
}
