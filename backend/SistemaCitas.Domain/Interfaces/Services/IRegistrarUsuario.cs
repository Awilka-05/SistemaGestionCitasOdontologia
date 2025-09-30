using SistemaCitas.Domain.Entities;
using SistemaCitas.Domain.Result_Pattern;

namespace SistemaCitas.Domain.Interfaces.Services
{
    public interface IRegistrarUsuario
    {
        Task<Result<Usuario>> RegistrarUsuarioAsync(Usuario usuario);
    }
}