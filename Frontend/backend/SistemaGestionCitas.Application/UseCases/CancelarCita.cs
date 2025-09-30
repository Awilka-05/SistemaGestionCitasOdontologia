﻿
using Microsoft.Extensions.Logging;
using SistemaGestionCitas.Application.Services;
using SistemaGestionCitas.Application.Validators;
using SistemaGestionCitas.Domain.Entities;
using SistemaGestionCitas.Domain.Enums;
using SistemaGestionCitas.Domain.Interfaces.Repositories;
using SistemaGestionCitas.Domain.Interfaces.Services;
using SistemaGestionCitas.Domain.Result_Pattern;
using SistemaGestionCitas.Infrastructure.Services.Correo.Factory;
using SistemaGestionCitas.Infrastructure.Services.Correo.Strategy;

namespace SistemaGestionCitas.Application.UseCases
{
    public class CancelarCita : ICancelarCitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly ICitaValidator _citaValidator;
        private readonly ILogger<CancelarCita> _logger;
        public CancelarCita(ICitaRepository citaRepository, ICitaValidator citaValidator, ILogger<CancelarCita> logger)
        {
            _citaRepository = citaRepository;
            _citaValidator = citaValidator;
            _logger = logger;
        }
        public async Task<Result<Cita>> CancelarCitaAsync(int citaId, Usuario usuario)
        {
            var validacion = await _citaValidator.ValidarCancelacionAsync(citaId, usuario);
            if (validacion.IsFailure)
            {
                _logger.LogWarning("No se pudo cancelar la cita con ID {CitaId}: {Error}", citaId, validacion.Error);
                return Result<Cita>.Failure(validacion.Error);
            }

            var cita = validacion.Value;
            cita.Estado = EstadoCita.Cancelada;
            await _citaRepository.UpdateAsync(cita);

            var citaCompleta = await _citaRepository.GetByIdAsync(cita.IdCita);

            if (citaCompleta == null || citaCompleta.ConfiguracionTurno == null || citaCompleta.ConfiguracionTurno.Franjas == null)
            {
                _logger.LogError("Fallo al cargar la cita completa para el correo de cancelación.");
                return Result<Cita>.Success(cita);
            }

            var franjaSeleccionada = citaCompleta.ConfiguracionTurno.Franjas
                                                .FirstOrDefault(f => f.FranjaId == citaCompleta.FranjaId);

            if (franjaSeleccionada == null)
            {
                _logger.LogError("No se encontró la franja horaria para la cita cancelada.");
                return Result<Cita>.Success(cita);
            }

            var resultadoEstrategia = CorreoEstrategiaFactory.FactoryCorreo("cancelacion");

            if (resultadoEstrategia.IsSuccess)
            {
                var estrategiaCancelacion = resultadoEstrategia.Value;
                var context = new CorreoContext();
                context.SetStrategy(estrategiaCancelacion);

                await context.EjecutarAsync(citaCompleta, usuario, franjaSeleccionada);
            }
            else
            {
                _logger.LogError("No se pudo crear la estrategia de correo: {Error}", resultadoEstrategia.Error);
            }

            return Result<Cita>.Success(cita);
        }
    }
    
    
}
