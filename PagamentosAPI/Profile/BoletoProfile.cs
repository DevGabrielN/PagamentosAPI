namespace PagamentosApi.Profile;
using AutoMapper;
using PagamentosApi.Data.Dtos;
using PagamentosApi.Models;
#pragma warning disable 1591
public class BoletoProfile : Profile
{
    public BoletoProfile()
    {
        CreateMap<CreateBoletoDto, Boleto>();
        CreateMap<CreateBoletoDto, ReadBoletoDto>();
        CreateMap<Boleto, ReadBoletoDto>();
        CreateMap<Boleto, ReadBoletoForBancoDto>();
        CreateMap<ReadBoletoForBancoDto, ReadBoletoDto>();
    }
}

