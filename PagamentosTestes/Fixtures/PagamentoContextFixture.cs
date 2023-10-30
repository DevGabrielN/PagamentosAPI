using Microsoft.EntityFrameworkCore;
using PagamentosApi.Data;

namespace PagamentosTestes.Fixtures;

public class PagamentoContextFixture : IDisposable
{
    public PagamentoContext Context { get; private set; }

    public PagamentoContextFixture()
    {
        var dbContextOptions = new DbContextOptionsBuilder<PagamentoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new PagamentoContext(dbContextOptions);        
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
