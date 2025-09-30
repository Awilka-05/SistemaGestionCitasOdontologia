using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionCitas.Application.DTOs.Responses
{
    public class DoctorResponseDto
    {
        public short DoctorId { get; set; }
        public string Nombre { get; set; } = null!;

    }
}
