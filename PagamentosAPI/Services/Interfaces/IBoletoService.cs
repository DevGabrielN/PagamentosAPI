using PagamentosApi.Data.Dtos;
#pragma warning disable 1591
namespace PagamentosApi.Services.Interfaces;

public interface IBoletoService<T>
{
    Task<ReadBoletoDto> CalculaJuros(ReadBoletoDto readBoletoDto);
    Task<T> CalculaJurosAsync(T boletoDto);
}
