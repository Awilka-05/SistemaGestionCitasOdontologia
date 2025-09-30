using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SistemaCitas.Application.Services;
using SistemaCitas.Application.UseCases;
using SistemaCitas.Application.Validators;
using SistemaCitas.Domain.Entities;
using SistemaCitas.Domain.Interfaces.Repositories;
using SistemaCitas.Domain.Interfaces.Services;
using SistemaCitas.Infrastructure.JWT;
using SistemaCitas.Infrastructure.Persistence.BdContext;
using SistemaCitas.Infrastructure.Repositories;
using SistemaCitas.Infrastructure.Repositories.Logger;
using SistemaCitas.Infrastructure.Services.Correo.Strategy;
using SistemaCitas.Domain.Value_Objects;
using SistemaCitas.Application.DTOs.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<SistemaCitasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de Aplicación/Casos de Uso (Services)
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<ICitaService, CitaService>();
builder.Services.AddScoped<IReservarCitaService, ReservarCita>();
builder.Services.AddScoped<ICancelarCitaService, CancelarCita>();
builder.Services.AddScoped<IConfiguracionTurnoService, ConfiguracionTurnoService>();
builder.Services.AddScoped<IHorarioService, HorarioService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRegistrarUsuario, RegistrarUsuario>();


// Registro de Validadores
builder.Services.AddScoped<ICitaValidator, CitaValidator>();


// Registra los repositorios concretos 
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();
builder.Services.AddScoped<IHorarioRepository, HorarioRepository>();
builder.Services.AddScoped<IConfiguracionTurnoRepository, ConfiguracionTurnoRepository>();


//Registros adicionales si usas un repositorio genérico
builder.Services.AddScoped<IRepository<Doctor, short>, DoctorRepository>();
builder.Services.AddScoped<IRepository<Servicio, short>, ServicioRepository>();
builder.Services.AddScoped<IRepository<Horario, short>, HorarioRepository>();
builder.Services.AddScoped<IRepository<ConfiguracionTurno, int>, ConfiguracionTurnoRepository>();

TypeAdapterConfig<CrearUsuarioDto, Usuario>.NewConfig()
    .Map(dest => dest.Nombre, src => Nombre.Create(src.Nombre).Value)
    .Map(dest => dest.Cedula, src => Cedula.Create(src.Cedula).Value)
    .Map(dest => dest.Correo, src => Correo.Create(src.Correo).Value)
    .Map(dest => dest.Telefono, src => src.Telefono)
    .Map(dest => dest.Contrasena, src => src.Contrasena)
    .Map(dest => dest.FechaNacimiento, src => src.FechaNacimiento)
    .Map(dest => dest.Rol, src => src.Rol);


// JWT & Auth
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddControllers().AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
 });



CorreoSender.SetConfiguration(builder.Configuration);
// My SingletonLogger
builder.Logging.AddConsole(); // consola sigue activa
builder.Logging.AddProvider(new SingletonLoggerProvider()); // nuestro logger singleton a archivo

//Mapster 

builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
builder.Services.AddScoped<IMapper, ServiceMapper>();


// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();