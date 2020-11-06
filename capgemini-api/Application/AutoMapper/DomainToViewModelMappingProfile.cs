using AutoMapper;
using capgemini_api.Application.Models.DTO;
using capgemini_api.Application.Models.ViewModel;
using capgemini_api.Domain.Models.Entitys;

namespace capgemini_api.Application.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMapForImportacaoRawDTOFromImportacao();
      CreateMapForImportacaoViewModelFromImportacao();
    }

    private void CreateMapForImportacaoRawDTOFromImportacao()
    {
      CreateMap<Importacao, ImportacaoRawDTO>()
          .ForMember(d => d.DataEntrega, m => m.MapFrom(d => d.DataEntrega.ToString("dd/MM/yyyy")))
          .ForMember(d => d.Quantidade, m => m.MapFrom(s => s.Quantidade.ToString()))
          .ForMember(d => d.ValorUnitario, m => m.MapFrom(s => s.ValorUnitario.ToString()));
    }

    private void CreateMapForImportacaoViewModelFromImportacao()
    {
      CreateMap<Importacao, ImportacaoViewModel>();
    }
  }
}
