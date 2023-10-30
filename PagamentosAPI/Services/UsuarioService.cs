using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PagamentosApi.Data.Dtos;
using PagamentosApi.Models;
using PagamentosApi.Services.Interfaces;
#pragma warning disable 1591

namespace PagamentosApi.Services;

public class UsuarioService : IUsuarioService
{
    private readonly SignInManager<Usuario> _signInManager;
    private readonly IMapper _mapper;
    private readonly UserManager<Usuario> _userManager;
    private readonly ITokenService _tokenSerive;    

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenSerive)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenSerive = tokenSerive;
    }

    public async Task CadastraAsync(CreateUsuarioDto dto)
    {
        Usuario usuario = _mapper.Map<Usuario>(dto);
        var result = await _userManager.CreateAsync(usuario, dto.Password);

        if (!result.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar o usuário:\n"
            + string.Join(",\n", result.Errors.Select(x => x.Description).ToList()));
        }
    }

    public async Task<string> LoginAsync(LoginUsuarioDto dto)
    {
        var result = await
            _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Usuario não autenticado");
        }
        var usuario = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

        return _tokenSerive.GenerateToken(usuario);
    }
}
