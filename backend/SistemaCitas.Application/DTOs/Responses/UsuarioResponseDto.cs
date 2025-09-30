using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCitas.Domain.Enums;
using SistemaCitas.Domain.Value_Objects;

namespace SistemaCitas.Application.DTOs.Responses
{
    public class UsuarioResponseDto
    {
        public static int IdUsuario { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public required string Nombre { get; set; }
        public required string Cedula { get; set; }
        public required string Correo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public RolUsuario Rol { get; set; }

    }
}
