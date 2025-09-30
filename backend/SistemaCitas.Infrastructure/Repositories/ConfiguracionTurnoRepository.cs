using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaCitas.Domain.Entities;
using SistemaCitas.Domain.Interfaces.Repositories;
using SistemaCitas.Infrastructure.Persistence.BdContext;

namespace SistemaCitas.Infrastructure.Repositories
{
    public class ConfiguracionTurnoRepository : IConfiguracionTurnoRepository
    {
        private readonly SistemaCitasDbContext _context;
        public ConfiguracionTurnoRepository(SistemaCitasDbContext context)
        {
            _context = context;
        }
        public async Task<ConfiguracionTurno?> GetByIdAsync(int id) {

            return await _context.ConfiguracionesTurnos
           .Include(c => c.Horario)
           .Include(c => c.Franjas)
           .FirstOrDefaultAsync(c => c.TurnoId == id);

        }
        public async Task<IEnumerable<ConfiguracionTurno>> GetAllAsync()
        {
            return await _context.ConfiguracionesTurnos
                                .Include(ct => ct.Horario)
                                .ToListAsync();
        }
        public async Task AddAsync(ConfiguracionTurno entity)
        {
            await _context.ConfiguracionesTurnos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ConfiguracionTurno entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
     
    }
}
