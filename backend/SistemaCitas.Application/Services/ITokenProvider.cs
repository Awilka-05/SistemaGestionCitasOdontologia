using SistemaCitas.Domain.Entities;

namespace SistemaCitas.Application.Services;

public interface ITokenProvider
{
    public string Create(Usuario usuario);
}