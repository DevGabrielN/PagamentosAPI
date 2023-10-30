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
/// Controller responsável pelas operações realacionadas a entidade boleto
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class BoletoController : Controller
{
    private record NewRecord(int Id);
    private readonly IMapper _mapper;
    private readonly PagamentoContext _pagamentoContext;
    private readonly IBoletoService<ReadBoletoDto> _boletoService;
    
    /// <summary>
    /// Construtor da classe BoletoController
    /// </summary>
    /// <param name="mapper">Serviço de mapeamento</param>
    /// <param name="pagamentoContext">Contexto do banco de dados de pagamentos</param>
    /// <param name="boletoService">Serviço de boletos</param>   
    public BoletoController(IMapper mapper, PagamentoContext pagamentoContext, IBoletoService<ReadBoletoDto> boletoService)
    {
        _mapper = mapper;
        _pagamentoContext = pagamentoContext;
        _boletoService = boletoService;
    }

    /// <summary>
    /// Cria um boleto
    /// </summary>    
    /// <returns>IActionResult</returns>
    /// <param name="boletoDto">Objeto com os campos necessários para criação de um boleto</param>
    /// <response code="201">Criado com sucesso</response>
    /// <response code="400">Bad request</response>
    /// <response code="401">Não autorizado</response>
    /// <response code="404">Banco não encontrado</response>  
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateBoleto([FromBody] CreateBoletoDto boletoDto)
    {
        var banco = await _pagamentoContext.Bancos.Where(b => b.Id == boletoDto.BancoId).FirstOrDefaultAsync();
        var test = await _pagamentoContext.Bancos.ToListAsync();

        if (banco == null)
        {
            return NotFound("Banco não encontrado!");
        }
        Boleto boleto = _mapper.Map<Boleto>(boletoDto);
        await _pagamentoContext.Boletos.AddAsync(boleto);
        try
        {
            await _pagamentoContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest("Boleto já cadastrado anteriormente! " + ex.InnerException);
        }

        var readBoleto = _mapper.Map<ReadBoletoDto>(boleto);
        return CreatedAtAction(nameof(GetBoletoByCodigo), new NewRecord(boleto.Id), readBoleto);
    }
    
    /// <summary>
    /// Recupera um boleto por id
    /// </summary>
    /// <param name="id">Id do boleto</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Requisição concluída com sucesso</response>
    /// <response code="401">Não autorizado</response>
    /// <response code="404">Não encontrado</response>    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBoletoByCodigo(int id)
    {
        var boleto =  await _pagamentoContext.Boletos.Where(b => b.Id == id).FirstOrDefaultAsync();
        if (boleto == null)
        {
            return NotFound();
        }
        var boletoDto = _mapper.Map<ReadBoletoDto>(boleto);

        
        return Ok(await _boletoService.CalculaJurosAsync(boletoDto));
    }
}
