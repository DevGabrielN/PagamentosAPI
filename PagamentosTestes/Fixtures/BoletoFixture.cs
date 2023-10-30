using PagamentosApi.Data.Dtos;
using PagamentosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentosTestes.Fixtures;

internal class BoletoFixture
{
    public static CreateBoletoDto GetCreateBoletoDtoTest(int bancoId) =>
    new()
    {
        BancoId = bancoId,
        CPFOrCNPJ = "11027735061",
        DataVencimento = DateTime.Now.Date,
        NomeBeneficiario = "Arlindo dos Santos",
        NomePagador = "José Pereira",
        Observacao = "Obs",
        Valor = 24.4
    };

    public static void ConfigurarBoletoVencido(Boleto boleto)
    {
        boleto.DataVencimento = DateTime.Now.Date.AddDays(-1);
    }


    public static double CalcularValorComJuros(ReadBoletoDto boletoDto, Banco banco)
    {
        return boletoDto.Valor + boletoDto.Valor * banco.PorcentualDeJuros;
    }
}
