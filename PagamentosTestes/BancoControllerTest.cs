namespace PagamentosTestes;

public class BancoControllerTest : IClassFixture<PagamentoContextFixture>
{
    private readonly PagamentoContext _pagamentoContext;    
    private readonly IMapper _mapper;
    private readonly IBoletoService<ReadBoletoForBancoDto> _boletoService;

    public BancoControllerTest(PagamentoContextFixture fixture)
    {
        _pagamentoContext = fixture.Context;
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BancoProfile>();

        });

        _mapper = mapperConfig.CreateMapper();
        _boletoService = new BoletoService<ReadBoletoForBancoDto>(_pagamentoContext);
    }

    [Fact]
    public async Task CreateBanco_Returns_CreatedAtActionResult()
    {
        // Arrange
        var banco = BancoFixture.GetCreateBancoDtoTest();
        var controller = new BancoController(_mapper, _pagamentoContext, _boletoService);

        // Act
        var result = await controller.CreateBanco(banco);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
        var createdResult = result as CreatedAtActionResult;
        Assert.NotNull(createdResult);
        Assert.Equal(201, createdResult.StatusCode);
        Assert.NotNull(createdResult.Value);

    }

    [Fact]
    public async Task GetBancoByCodigo_Returns_OkObjectResult()
    {
        // Arrange
        var controller = new BancoController(_mapper, _pagamentoContext, _boletoService);
        var banco = BancoFixture.GetOnlyBancoTest();
        _pagamentoContext.Bancos.Add(banco);
        _pagamentoContext.SaveChanges();

        // Act
        var result = await controller.GetBancoByCodigo(banco.CodigoDoBanco);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        var content = okResult?.Value;
        Assert.NotNull(content);

    }

    [Fact]
    public async Task GetAllBancos_Returns_OkObjectResult()
    {
        // Arrange
        var controller = new BancoController(_mapper, _pagamentoContext, _boletoService);
        _pagamentoContext.Bancos.AddRange(BancoFixture.GetManyBancosTest());
        _pagamentoContext.SaveChanges();

        // Act
        var result = await controller.GetAllBancos();

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        var content = okResult?.Value;
        Assert.NotNull(content);
    }

}