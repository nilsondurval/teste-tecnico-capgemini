using AutoMapper;
using capgemini_api.Models.DTO;
using capgemini_api.Models.Entitys;
using System;

namespace capgemini_api.Application.AutoMapper
{
  public class ViewModelToDomainMappingProfile : Profile
  {
    public ViewModelToDomainMappingProfile()
    {
      CreateMapForImportacao();
    }      

    private void CreateMapForImportacao()
    {
       CreateMap<ImportacaoDTO, Importacao>()
        .ForMember(d => d.DataEntrega, m => m.MapFrom(d => Convert.ToDateTime(d.DataEntrega)))
        .ForMember(d => d.Quantidade, m => m.MapFrom(s => Convert.ToInt32(s.Quantidade)))
        .ForMember(d => d.ValorUnitario, m => m.MapFrom(s => Math.Round(Convert.ToDecimal(s.ValorUnitario), 2)));
    }
  }
}
