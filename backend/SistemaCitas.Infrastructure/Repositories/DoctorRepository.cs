
using Microsoft.EntityFrameworkCore;
using SistemaCitas.Domain.Entities;
using SistemaCitas.Domain.Interfaces.Repositories;
using SistemaCitas.Infrastructure.Persistence.BdContext;

namespace SistemaCitas.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly SistemaCitasDbContext _context;

        public DoctorRepository(SistemaCitasDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor?> GetByIdAsync(short id)
        {
             return await _context.Set<Doctor>().FindAsync(id);

        }
           

        public async Task<IEnumerable<Doctor>> GetAllAsync() {

            return await _context.Set<Doctor>().ToListAsync();
        }
           

        public async Task AddAsync(Doctor entity)
        {
            await _context.Set<Doctor>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor entity)
        {
            var existingEntity = _context.Set<Doctor>()
            .Local
            .FirstOrDefault(e => e.DoctorId == entity.DoctorId);

            if (existingEntity != null)
            {
 
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Set<Doctor>().Update(entity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.Set<Doctor>().AnyAsync(l => l.Nombre == nombre);
        }
    }
}
