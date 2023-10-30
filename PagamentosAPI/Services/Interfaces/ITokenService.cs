using PagamentosApi.Models;
#pragma warning disable 1591

namespace PagamentosApi.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
}
