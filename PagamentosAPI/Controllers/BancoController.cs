using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagamentosApi.Data;
using PagamentosApi.Data.Dtos;
using PagamentosApi.Models;
using PagamentosApi.Services.Interfaces;

namespace PagamentosApi.Controllers;

/// <summary>
/// Controller responsável pelas operações realacionadas a entidade banco
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class BancoController : ControllerBase
{
    private record NewRecord(string codigoBanco);
    private readonly IMapper _mapper;
    private readonly PagamentoContext _pagamentoContext;
    private readonly IBoletoService<ReadBoletoForBancoDto> _boletoService;

    /// <summary>
    /// Construtor da classe BoletoController.
    /// </summary>
    /// <param name="mapper">Serviço de mapeamento.</param>
    /// <param name="pagamentoContext">Contexto do banco de dados de pagamentos.</param>
    /// <param name="boletoService">Serviço de boletos</param>
    public BancoController(IMapper mapper, PagamentoContext pagamentoContext, IBoletoService<ReadBoletoForBancoDto> boletoService)
    {
        _mapper = mapper;
        _pagamentoContext = pagamentoContext;
        _boletoService = boletoService;
    }
    /// <summary>
    /// Cria um banco
    /// </summary>    
    /// <returns>IActionResult</returns>
    /// <response code="201">Criado com sucesso</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Não autorizado</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]    
    public async Task<IActionResult> CreateBanco([FromBody] CreateBancoDto bancoDto)
    {
        Banco banco = _mapper.Map<Banco>(bancoDto);
        await _pagamentoContext.Bancos.AddAsync(banco);
        try
        {
            _pagamentoContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest("Banco já cadastrado anteriormente! " + ex.InnerException);
        }

        return CreatedAtAction(nameof(GetBancoByCodigo), new NewRecord(banco.CodigoDoBanco), banco);
    }
    
    /// <summary>
    /// Recupera um banco por código do banco
    /// </summary>    
    /// <returns>IActionResult</returns>
    /// <response code="200">Requisição concluída com sucesso</response>
    /// <response code="401">Não autorizado</response>
    /// <response code="404">Não encontrado</response>    
    [HttpGet("{codigoBanco}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBancoByCodigo(string codigoBanco)
    {
        var banco = await _pagamentoContext.Bancos.Where(x => x.CodigoDoBanco == codigoBanco).FirstOrDefaultAsync();
        
        if (banco == null)
        {
            return NotFound();
        }
        var bancoDto = _mapper.Map<ReadBancoDto>(banco);
        
        var task = Task.Factory.StartNew(() => bancoDto.Boletos.ForEach(async boleto => await _boletoService.CalculaJurosAsync(boleto)));

        await task;

        return Ok(bancoDto);
    }

    
    /// <summary>
    /// Recupera uma lista de bancos
    /// </summary>    
    /// <returns>IActionResult</returns>
    /// <response code="200">Requisição concluída com sucesso</response>
    /// <response code="401">Não autorizado</response>
    /// <response code="404">Não encontrado</response>    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    
    public async Task<IActionResult> GetAllBancos()
    {
        var bancos = await _pagamentoContext.Bancos.ToListAsync();
        if (bancos == null)
        {
            return NotFound();
        }

        var bancosDto = _mapper.Map<List<ReadBancoDto>>(bancos);
        foreach (var bancoDto in bancosDto)
        {
            foreach (var boleto in bancoDto.Boletos)
            {
                await _boletoService.CalculaJurosAsync(boleto);
            }
        };

        return Ok(bancosDto);
    }
}


