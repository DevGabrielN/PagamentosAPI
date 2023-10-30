using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable 1591
namespace PagamentosApi.Models;

[Index(nameof(CodigoDoBanco), IsUnique = true)]
[Index(nameof(NomeDoBanco), IsUnique = true)]
public class Banco
{
    [Key]
    [Required]
    public int Id { get; set; }
    public virtual ICollection<Boleto> Boletos { get; set; }

    [NotMapped]
    public string _nomeDoBanco;

    [Required(ErrorMessage = "{0} is required")]
    public string NomeDoBanco
    {
        get => _nomeDoBanco;
        set => _nomeDoBanco = value.Trim().ToUpper();
    }

    [Required(ErrorMessage = "{0} is required")]
    public string CodigoDoBanco { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public double PorcentualDeJuros { get; set; }

    public Banco()
    {
    }
}
