using AutoMapper;
using capgemini_api.Application.Models.DTO;
using capgemini_api.Application.Models.ViewModel;
using capgemini_api.Domain.Models.Entitys;
using System;

namespace capgemini_api.Application.AutoMapper
{
  public class ViewModelToDomainMappingProfile : Profile
  {
    public ViewModelToDomainMappingProfile()
    {
      CreateMapForImportacaoFromImportacaoRawDTO();
    }      

    private void CreateMapForImportacaoFromImportacaoRawDTO()
    {
       CreateMap<ImportacaoRawDTO, Importacao>()
        .ForMember(d => d.DataEntrega, m => m.MapFrom(d => Convert.ToDateTime(d.DataEntrega)))
        .ForMember(d => d.Quantidade, m => m.MapFrom(s => Convert.ToInt32(s.Quantidade)))
        .ForMember(d => d.ValorUnitario, m => m.MapFrom(s => Math.Round(Convert.ToDecimal(s.ValorUnitario), 2)));
    }

    private void CreateMapForImportacaoFromImportacaoViewModel()
    {
       CreateMap<ImportacaoViewModel, Importacao>();
    }
  }
}
