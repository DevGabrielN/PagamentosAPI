using Microsoft.AspNetCore.Mvc;
using PagamentosApi.Data.Dtos;
using PagamentosApi.Services;

namespace PagamentosApi.Controllers;
/// <summary>
/// Controller responsável por realizar operações de autorização da classe Usuario
/// </summary>
[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{

    private UsuarioService _usuarioService;

    /// <summary>
    /// Construtor da classe UsuarioController
    /// </summary>
    /// <param name="cadastroService">Classe de contexto do Identity</param>
    public UsuarioController(UsuarioService cadastroService)
    {
        _usuarioService = cadastroService;
    }

    [HttpPost("Cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        await _usuarioService.CadastraAsync(dto);
        return Ok("Usuário cadastrado");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUsuarioDto dto)
    {
        var token = await _usuarioService.LoginAsync(dto);
        return Ok(token);
    }

}
