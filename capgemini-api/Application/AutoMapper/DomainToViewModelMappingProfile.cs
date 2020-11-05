using AutoMapper;
using capgemini_api.Models.DTO;
using capgemini_api.Models.Entitys;

namespace capgemini_api.Application.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMapForImportacaoDTO();
    }

    private void CreateMapForImportacaoDTO()
    {
     CreateMap<Importacao, ImportacaoDTO>()
        .ForMember(d => d.DataEntrega, m => m.MapFrom(d => d.DataEntrega.ToString("dd/MM/yyyy")))
        .ForMember(d => d.Quantidade, m => m.MapFrom(s => s.Quantidade.ToString()))
        .ForMember(d => d.ValorUnitario, m => m.MapFrom(s => s.ValorUnitario.ToString()));
    }
  }
}
