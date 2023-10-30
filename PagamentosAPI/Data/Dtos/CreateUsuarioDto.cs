using System.ComponentModel.DataAnnotations;
#pragma warning disable 1591

namespace PagamentosApi.Data.Dtos;

public class CreateUsuarioDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string RePassword { get; set; }
}
