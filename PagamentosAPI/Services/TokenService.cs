using Microsoft.IdentityModel.Tokens;
using PagamentosApi.Models;
using PagamentosApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
#pragma warning disable 1591

namespace PagamentosApi.Services;

public class TokenService : ITokenService
{
    private IConfiguration _configuration { get; set; }
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;

    }
    public string GenerateToken(Usuario usuario)
    {
        Claim[] claims = new Claim[]
        {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),                
        };
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(60),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
