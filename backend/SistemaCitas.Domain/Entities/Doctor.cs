using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCitas.Domain.Entities
{
    public class Doctor
    {
        public short DoctorId { get; set; }
        public string Nombre { get; set; } = null!;

        public ICollection<Cita> Citas { get; set; } = null!;
    }
}