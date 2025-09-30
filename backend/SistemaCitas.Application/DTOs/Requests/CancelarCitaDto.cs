using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCitas.Application.Validators;

namespace SistemaCitas.Application.DTOs.Requests
{
    public class CancelarCitaDto
    {
        [IDValido(ErrorMessage = "El ID de la Cita no es valido.")]
        public int IdCita { get; set; }

        [IDValido(ErrorMessage = "El ID de la Cita no es valido.")]
        public int IdUsuario { get; set; }
    }
}
