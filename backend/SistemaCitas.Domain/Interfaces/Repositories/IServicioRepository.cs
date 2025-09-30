using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCitas.Domain.Entities;

namespace SistemaCitas.Domain.Interfaces.Repositories
{
    public interface IServicioRepository : IRepository<Servicio, short>
    {
    }
}