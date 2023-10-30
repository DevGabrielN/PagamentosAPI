using PagamentosApi.Data.Dtos;
using PagamentosApi.Models;

namespace PagamentosTestes.Fixtures;

public static class BancoFixture
{
    public static List<Banco> GetManyBancosTest() =>
        new() {
            new Banco
            {
                CodigoDoBanco = "123",
                NomeDoBanco = "Banco_test1",
                PorcentualDeJuros = 2
            },
            new Banco
            {
                CodigoDoBanco = "124",
                NomeDoBanco = "Banco_test2",
                PorcentualDeJuros = 0.2
            },
            new Banco
            {
                CodigoDoBanco = "125",
                NomeDoBanco = "Banco_test3",
                PorcentualDeJuros = 0.5
            },
            new Banco
            {
                CodigoDoBanco = "126",
                NomeDoBanco = "Banco_test4",
                PorcentualDeJuros = 0.8
            },
            new Banco
            {
                CodigoDoBanco = "226",
                NomeDoBanco = "Banco_test5",
                PorcentualDeJuros = 1.8
            },
            new Banco
            {
                CodigoDoBanco = "225",
                NomeDoBanco = "Banco_test6",
                PorcentualDeJuros = 2.8
            },
        };

    public static Banco GetOnlyBancoTest() =>
        new()
        {
            CodigoDoBanco = "449",
            NomeDoBanco = "Banco_test7",
            PorcentualDeJuros = 0.8
        };

    public static CreateBancoDto GetCreateBancoDtoTest() =>
        new()
        {
            CodigoDoBanco = "785",
            NomeDoBanco = "Banco_test8",
            PorcentualDeJuros = 0.9
        };
}
