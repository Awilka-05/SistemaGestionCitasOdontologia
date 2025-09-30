﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionCitas.Domain.Entities
{
    public class ConfiguracionTurno
    {
        public int TurnoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public short HorariosId { get; set; }
        public int CantidadEstaciones { get; set; }
        public int DuracionMinutos { get; set; }
        public bool AunAceptaCitas { get; set; }

        public Horario Horario { get; set; } = null!;
        public ICollection<FranjaHorario> Franjas { get; set; } = new List<FranjaHorario>();
        public ICollection<Cita> Citas { get; set; } = null!;
    }
}