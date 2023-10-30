using PagamentosApi.Models;
#pragma warning disable 1591

namespace PagamentosApi.Data.Dtos;

public class ReadBancoDto
{
    public int Id { get; set; }    
    public string NomeDoBanco { get; set; }   
    public string CodigoDoBanco { get; set; }    
    public double PorcentualDeJuros { get; set; }
    public List<ReadBoletoForBancoDto> Boletos { get; set; }
}
