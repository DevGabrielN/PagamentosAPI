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
    /// <summary>
    /// Cadastrar usuário
    /// </summary>
    /// <param name="dto">Objeto necessário para cadastrar um usuário</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Requisição concluída com sucesso</response>
    /// <response code="400">Bad Request</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        await _usuarioService.CadastraAsync(dto);
        return Ok("Usuário cadastrado");
    }
    /// <summary>
    /// Realizar login
    /// </summary>
    /// <param name="dto">Objeto necessário para realizar login</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Requisição concluída com sucesso</response>
    /// <response code="400">Bad Request</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUsuarioDto dto)
    {
        var token = await _usuarioService.LoginAsync(dto);
        return Ok(token);
    }

}
