using Microsoft.EntityFrameworkCore;
using PagamentosApi.Data;
using PagamentosApi.Data.Dtos;
using PagamentosApi.Models;
using PagamentosApi.Services.Interfaces;
#pragma warning disable 1591
namespace PagamentosApi.Services;

public class BoletoService<T> : IBoletoService<T>
{
    public readonly PagamentoContext _pagamentoContext;

    public BoletoService(PagamentoContext pagamentoContext)
    {
        _pagamentoContext = pagamentoContext;
    }

    public async Task<ReadBoletoDto> CalculaJuros(ReadBoletoDto readBoletoDto)
    {
        if (readBoletoDto.DataVencimento.Date < DateTime.Now.Date)
        {
            var banco = await _pagamentoContext.Bancos.Where(b => b.Id == readBoletoDto.BancoId).FirstAsync();
            double valorComJuros = readBoletoDto.Valor + (readBoletoDto.Valor * banco.PorcentualDeJuros);
            // Atualiza o valor no DTO
            readBoletoDto.Valor = valorComJuros;
        }

        return readBoletoDto;
    }

    public async Task<T> CalculaJurosAsync(T boletoDto)
    {
        dynamic? boleto = null;
        if (boletoDto is ReadBoletoDto)
        {
            boleto = boletoDto as ReadBoletoDto;
        }
        else if (boletoDto is ReadBoletoForBancoDto)
        {
            boleto = boletoDto as ReadBoletoForBancoDto;
        }
        else
        {
            throw new ArgumentException("Operação não suportada para o objeto");
        }
        int id = boleto?.Id;

        if (boleto?.DataVencimento.Date < DateTime.Now.Date)
        {
            var banco = await _pagamentoContext.Boletos
            .Where(b => b.Id == id)
            .Join(
                _pagamentoContext.Bancos,
                boleto => boleto.BancoId,
                banco => banco.Id,
                (boleto, banco) => banco
            )
            .FirstOrDefaultAsync();
            double valorComJuros = boleto.Valor + (boleto.Valor * banco?.PorcentualDeJuros);
            // Atualiza o valor no DTO
            boleto.Valor = valorComJuros;
        }

        return boleto;
    }
}
