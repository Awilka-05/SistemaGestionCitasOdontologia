using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaCitas.Domain.Entities;

namespace SistemaCitas.Infrastructure.Services.Correo.Strategy
{
    public class CorreoCancelacionEstrategia : ICorreoEstrategia
    {
        public async Task EnviarAsync(Cita cita, Usuario usuario, FranjaHorario franjaSeleccionada)
        {
            string template = await File.ReadAllTextAsync("C:\\SistemaCitas\\backend\\SistemaGestionCitas.Infrastructure\\Services\\Correo\\Plantillas\\cancelacion.html");
            string body = template
                .Replace("{{UserName}}", usuario.Nombre.Value)
                .Replace("{{IdCita}}", cita.IdCita.ToString())
                .Replace("{{Fecha}}", cita.FechaCita.ToString("dd/MM/yyyy"))
                .Replace("{{Servicio}}", cita.Servicio.Nombre)
                .Replace("{{Precio}}", cita.Servicio.Precio.ToString())
                 .Replace("{{HorarioInicio}}", franjaSeleccionada.HoraInicio.ToString("HH:mm"))
                .Replace("{{HorarioFin}}", franjaSeleccionada.HoraFin.ToString("HH:mm"))
                .Replace("{{Duracion}}", cita.ConfiguracionTurno.DuracionMinutos.ToString())
                .Replace("{{Doctor}}", cita.Doctor.Nombre);


            await CorreoSender.EnviarAsync(usuario.Correo.Value, "Cancelación de cita", body);
        }
        
    }
}
