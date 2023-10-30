using System.ComponentModel.DataAnnotations;
#pragma warning disable 1591

namespace PagamentosApi.Data.Dtos;

public class CreateBoletoDto
{
    
    [Required]
    public int BancoId { get; set; }

    /// <example>João da Silva</example>
    [RegularExpression(@"^[A-ZÁÀÃÉÊÍÓÔÕÚÇa-záàãéêíóôõúç]+(?: [A-ZÁÀÃÉÊÍÓÔÕÚÇa-záàãéêíóôõúç]+)+$", ErrorMessage = "Invalid name format")]
    [Required(ErrorMessage = "{0} is required")]
    public string NomePagador { get; set; }

    [ValidaCPFouCNPJ(ErrorMessage = "CPF or CNPJ is invalid!")]
    [RegularExpression("^(?:\\d{11}|\\d{14})$", ErrorMessage = "Must contain 11 or 14 numeric characters")]
    [Required(ErrorMessage = "{0} is required")]
    public string? CPFOrCNPJ { get; set; } = string.Empty;

    /// <example>Maria da Silva</example>
    [RegularExpression(@"^[A-ZÁÀÃÉÊÍÓÔÕÚÇa-záàãéêíóôõúç]+(?: [A-ZÁÀÃÉÊÍÓÔÕÚÇa-záàãéêíóôõúç]+)+$", ErrorMessage = "Invalid name format")]
    [Required(ErrorMessage = "{0} is required")]
    public string? NomeBeneficiario { get; set; } = string.Empty;

    [Range(10, double.MaxValue, ErrorMessage = "{0} must be greater than or equal to 10")]
    [Required(ErrorMessage = "{0} is required")]
    public double Valor { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [DataType(DataType.Date)]
    public DateTime DataVencimento { get; set; }

    /// <example></example>
    [MaxLength(255, ErrorMessage = "Max length is {1} caracteres")]
    public string? Observacao { get; set; } = string.Empty;

}

public class ValidaCPFouCNPJ : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext? validationContext)
    {
        var cpfOrCnpj = value as string;

        bool isCPF = ValidaCPF.IsCpf(cpfOrCnpj);
        bool isCNPJ = ValidaCNPJ.IsCnpj(cpfOrCnpj);

        if (isCNPJ || isCPF)
        {
            return ValidationResult.Success;
        }


        return new ValidationResult(ErrorMessage ?? "CPF ou CNPJ inválido!");
    }
}

public static class ValidaCNPJ
{
    public static bool IsCnpj(string cnpj)
    {
        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma;
        int resto;
        string digito;
        string tempCnpj;
        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
        if (cnpj.Length != 14)
            return false;
        tempCnpj = cnpj.Substring(0, 12);
        soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cnpj.EndsWith(digito);
    }
}

public static class ValidaCPF
{
    public static bool IsCpf(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cpf.EndsWith(digito);
    }
}
