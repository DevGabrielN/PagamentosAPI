using System.ComponentModel.DataAnnotations;
#pragma warning disable 1591

namespace PagamentosApi.Data.Dtos;

public class CreateBancoDto
{

    [Required(ErrorMessage = "{0} is required")]
    public string NomeDoBanco { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [RegularExpression("^[0-9]{3}$", ErrorMessage = "Only 3 numeric characters are allowed")]
    public string CodigoDoBanco { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public double PorcentualDeJuros { get; set; }
}
