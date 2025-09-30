using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionCitas.Domain.Enums;

namespace SistemaGestionCitas.Domain.Entities
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdUsuario { get; set; }
        public int TurnoId { get; set; }
        public short DoctorId { get; set; }
        public short ServicioId { get; set; }
        public int FranjaId { get; set; }
        public DateTime FechaCita { get; set; }
        public EstadoCita Estado { get; set; } 
        public byte[] RowVersion { get; set; } = null!;

        public Usuario Usuario { get; set; } = null!;
        public FranjaHorario FranjaHorario { get; set; } = null!;
        public ConfiguracionTurno ConfiguracionTurno { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public Servicio Servicio { get; set; } = null!;
    }
}
