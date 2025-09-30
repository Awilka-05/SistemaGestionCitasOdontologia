using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCitas.Application.DTOs.Requests;
using SistemaCitas.Domain.Entities;

namespace SistemaCitas.Application.DTOs.Responses
{
    public class ConfiguracionTurnoResponseDto
    {
        public int TurnoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DuracionMinutos { get; set; }
        public short HorarioId { get; set; }
        public int CantidadEstaciones { get; set; }

        // Lista de las franjas generadas automáticament
        public List<FranjaHorarioDto> Franjas { get; set; }
    }
    
}
