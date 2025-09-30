
using System.ComponentModel.DataAnnotations; 

namespace SistemaCitas.Application.DTOs.Requests
{
    public class DoctorDto
    {
        [Required(ErrorMessage = "El nombre del doctor es requerido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El nombre del doctorr debe tener entre 5 y 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre del doctor solo puede contener letras y espacios.")]
        public string Nombre { get; set; } = null!;
    }
}
