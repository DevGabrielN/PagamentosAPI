namespace PagamentosApi.Profile;
using AutoMapper;
using PagamentosApi.Data.Dtos;
using PagamentosApi.Models;
#pragma warning disable 1591

public class BancoProfile : Profile
{
    public BancoProfile()
    {
        CreateMap<CreateBancoDto, Banco>();
        CreateMap<Banco, ReadBancoDto>();
    }
}