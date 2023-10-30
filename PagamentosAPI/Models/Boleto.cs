using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable 1591
namespace PagamentosApi.Models;

public class Boleto
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int BancoId { get; set; }
    public virtual Banco Banco { get; set; }

    [Required]
    public string NomePagador { get; set; }

    [MaxLength(14)]
    [Required]
    public string CPFOrCNPJ { get; set; }

    [Required]
    public string NomeBeneficiario { get; set; }

    [Required]
    public double Valor { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime DataVencimento { get; set; }

    [MaxLength(255)]
    public string? Observacao { get; set; }

    public Boleto()
    {            
    }
}
