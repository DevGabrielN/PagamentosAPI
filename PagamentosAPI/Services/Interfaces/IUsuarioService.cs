using PagamentosApi.Data.Dtos;
#pragma warning disable 1591

namespace PagamentosApi.Services.Interfaces;
public interface IUsuarioService
{
    Task CadastraAsync(CreateUsuarioDto dto);
    Task<string> LoginAsync(LoginUsuarioDto dto);
}
