namespace PagamentosApi.Data.Dtos;
#pragma warning disable 1591
public class ReadBoletoForBancoDto
{   
    public int Id { get; set; }
    public string NomePagador { get; set; }
    public string CPFOrCNPJ { get; set; }
    public string NomeBeneficiario { get; set; }
    public double Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public string? Observacao { get; set; }
}
