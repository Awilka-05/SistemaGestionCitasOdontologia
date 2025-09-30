
using Microsoft.EntityFrameworkCore;
using SistemaCitas.Domain.Entities;
using SistemaCitas.Domain.Interfaces.Repositories;
using SistemaCitas.Infrastructure.Persistence.BdContext;

namespace SistemaCitas.Infrastructure.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly SistemaCitasDbContext  _context;

        public ServicioRepository(SistemaCitasDbContext context)
        {
            _context = context;
        }

        public async Task<Servicio?> GetByIdAsync(short id) =>
            await _context.Set<Servicio>().FindAsync(id);

        public async Task<IEnumerable<Servicio>> GetAllAsync() =>
            await _context.Set<Servicio>().ToListAsync();

        public async Task AddAsync(Servicio entity)
        {
            await _context.Set<Servicio>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Servicio entity)
        {
            var existingEntity = _context.Set<Servicio>()
            .Local
            .FirstOrDefault(e => e.ServicioId == entity.ServicioId);

            if (existingEntity != null)
            {

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Set<Servicio>().Update(entity);
            }

            await _context.SaveChangesAsync();

        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.Set<Servicio>().AnyAsync(s => s.Nombre == nombre);
        }
    }

}
