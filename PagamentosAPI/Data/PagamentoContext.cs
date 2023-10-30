using Microsoft.EntityFrameworkCore;
using PagamentosApi.Models;
#pragma warning disable 1591

namespace PagamentosApi.Data;
public class PagamentoContext : DbContext
{
    public PagamentoContext(DbContextOptions<PagamentoContext> opts) : base(opts)
    {
    }

    public DbSet<Boleto> Boletos { get; set; }
    public DbSet<Banco> Bancos { get; set; }
}
