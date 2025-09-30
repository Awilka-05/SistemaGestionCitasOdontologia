using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCitas.Application.DTOs.Requests
{
    public class FranjaHorarioDto
    {
        public int FranjaId { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public bool Disponible { get; set; } = true;
    }
}
