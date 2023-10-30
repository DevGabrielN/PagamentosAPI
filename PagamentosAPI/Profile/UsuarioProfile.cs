namespace PagamentosApi.Profile;
using AutoMapper;
using PagamentosApi.Data.Dtos;
using PagamentosApi.Models;
#pragma warning disable 1591
public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario>();
    }
}

