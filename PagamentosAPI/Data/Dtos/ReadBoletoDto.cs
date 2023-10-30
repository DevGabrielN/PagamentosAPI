using System.ComponentModel.DataAnnotations;
#pragma warning disable 1591

namespace PagamentosApi.Data.Dtos;

public class ReadBoletoDto
{
    public int Id { get; set; }
    public int BancoId { get; set; }   
    public string NomePagador { get; set; }  
    public string CPFOrCNPJ { get; set; }   
    public string NomeBeneficiario { get; set; }   
    public double Valor { get; set; }   
    public DateTime DataVencimento { get; set; }
    public string? Observacao { get; set; }
}
