namespace PagamentosTestes;

public class BoletoControllerTest : IClassFixture<PagamentoContextFixture>
{
    private readonly PagamentoContext _pagamentoContext;
    private readonly IMapper _mapper;
    private readonly IBoletoService<ReadBoletoDto> _boletoService;

    public BoletoControllerTest(PagamentoContextFixture fixture)
    {
        _pagamentoContext = fixture.Context;
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BoletoProfile>();
            cfg.AddProfile<BancoProfile>();

        });
        _boletoService = new BoletoService<ReadBoletoDto>(_pagamentoContext);
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateBoleto_Returns_CreatedAtActionResult()
    {
        // Arrange       
        Banco banco = _mapper.Map<Banco>(BancoFixture.GetCreateBancoDtoTest());        
        await _pagamentoContext.Bancos.AddAsync(banco);
        await _pagamentoContext.SaveChangesAsync();
        var controller = new BoletoController(_mapper, _pagamentoContext, _boletoService);
        var boleto = BoletoFixture.GetCreateBoletoDtoTest(banco.Id);

        // Act
        var result = await controller.CreateBoleto(boleto);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
        var createdResult = result as CreatedAtActionResult;
        Assert.NotNull(createdResult);
        Assert.Equal(201, createdResult.StatusCode);
        Assert.NotNull(createdResult.Value);
        
    }

    [Fact]
    public async Task GetBoletoById_Returns_CreatedAtActionResult()
    {
        // Arrange       
        Banco banco = _mapper.Map<Banco>(BancoFixture.GetCreateBancoDtoTest());
        await _pagamentoContext.Bancos.AddAsync(banco);       
        var controller = new BoletoController(_mapper, _pagamentoContext, _boletoService);
        var boletoDto = BoletoFixture.GetCreateBoletoDtoTest(banco.Id);
        Boleto boleto = _mapper.Map<Boleto>(boletoDto);        
        await _pagamentoContext.Boletos.AddAsync(boleto);
        await _pagamentoContext.SaveChangesAsync();
        // Act
        var result = await controller.GetBoletoByCodigo(boleto.Id);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task CalculaJurosParaBoletoVencido_DeveRetornarValorComJuros()
    {
        // Arrange
        Banco banco = _mapper.Map<Banco>(BancoFixture.GetCreateBancoDtoTest());
        await _pagamentoContext.Bancos.AddAsync(banco);

        Boleto boleto = _mapper.Map<Boleto>(BoletoFixture.GetCreateBoletoDtoTest(banco.Id));
        BoletoFixture.ConfigurarBoletoVencido(boleto);

        await _pagamentoContext.Boletos.AddAsync(boleto);
        await _pagamentoContext.SaveChangesAsync();

        ReadBoletoDto readBoletoDto = _mapper.Map<ReadBoletoDto>(boleto);

        var expectedValue = BoletoFixture.CalcularValorComJuros(readBoletoDto, banco);

        // Act
        var result = await _boletoService.CalculaJurosAsync(readBoletoDto);

        // Assert
        Assert.Equal(expectedValue, result.Valor, 2);
    }   
    
}
