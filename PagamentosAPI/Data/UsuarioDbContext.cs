using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PagamentosApi.Models;
#pragma warning disable 1591

namespace PagamentosApi.Data;

public class UsuarioDbContext : IdentityDbContext<Usuario>
{
    public UsuarioDbContext(DbContextOptions<UsuarioDbContext> opts) : base(opts)
    {
    }
}
