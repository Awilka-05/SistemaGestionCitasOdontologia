﻿
using Microsoft.EntityFrameworkCore;
using SistemaGestionCitas.Domain.Entities;
using SistemaGestionCitas.Domain.Enums;
using SistemaGestionCitas.Domain.Interfaces.Repositories;
using SistemaGestionCitas.Infrastructure.Persistence.BdContext;

namespace SistemaGestionCitas.Infrastructure.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly SistemaCitasDbContext _context;

        public CitaRepository(SistemaCitasDbContext context)
        {
            _context = context;
        }

        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _context.Citas
            .Include(c => c.Servicio)
            .Include(c => c.Doctor)
            .Include(c => c.ConfiguracionTurno)
                .ThenInclude(ct => ct.Horario)
            .Include(c => c.FranjaHorario) 
            .FirstOrDefaultAsync(c => c.IdCita == id);

        }
           

        public async Task<IEnumerable<Cita>> GetAllAsync(){

            return await _context.Set<Cita>()
            .Include(c => c.Servicio)
            .Include(c => c.Doctor)
            .Include(c => c.ConfiguracionTurno)
                .ThenInclude(ct => ct.Horario)
            .Include(c => c.FranjaHorario)
            .ToListAsync();
        }
            

        public async Task AddAsync(Cita entity)
        {
            await _context.Set<Cita>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cita entity)
        {
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Cita>> GetCitasByUsuarioAsync(int usuarioId)
        {
            return await _context.Set<Cita>()
           .Include(c => c.ConfiguracionTurno)
           .ThenInclude(ct => ct.Horario)
           .Include(c => c.FranjaHorario) 
           .Where(c => c.IdUsuario == usuarioId)
           .ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetByFechaAsync(DateTime fecha)
        {
            return await _context.Set<Cita>()
           .Include(c => c.ConfiguracionTurno)
           .Where(c => c.ConfiguracionTurno.FechaInicio.Date == fecha.Date)
           .ToListAsync();

        }
       

        public async Task<IEnumerable<Cita>> GetByEstadoAsync(EstadoCita estado) {
            
            return await _context.Set<Cita>()
                .Where(c => c.Estado == estado)
                .ToListAsync();
        }

        public async Task<int> CountByTurnoIdAsync(int turnoId)
        {
             return await _context.Citas.CountAsync(c => c.TurnoId == turnoId);
        }

        public async Task<int> CountByFranjaIdAsync(int franjaId, int turnoId, DateTime fecha)
        {
            return await _context.Citas
                .CountAsync(c => c.FranjaId == franjaId &&
                                 c.TurnoId == turnoId &&
                                 c.FechaCita.Date == fecha.Date);
        }
    }
}
