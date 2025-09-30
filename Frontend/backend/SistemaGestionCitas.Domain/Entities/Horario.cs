using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionCitas.Domain.Entities
{
    public class Horario
    {
        public short HorarioId { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string Descripcion { get; set; } = null!;

        public ICollection<ConfiguracionTurno> ConfiguracionesTurnos { get; set; } = null!;
    }
}