using capgemini_api.Infra.Data.Repository;
using capgemini_api.Infra.Services;
using capgemini_api.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using capgemini_api.Application.Services.Interfaces;
using capgemini_api.Domain.Services;

namespace GestaoDocumentosApi.Infra
{
  public class NativeInjectorBootStrapper
  {
    public static void RegisterServices(IServiceCollection services)
    {
      // Application
      services.AddScoped<IMultipartFormDataAppService, MultipartFormDataAppService>();
      services.AddScoped<ImportacaoAppService>();

      // Domain
      services.AddScoped<ImportacaoDomainService>();
     
      // Infra - Repository
      services.AddScoped<ImportacaoRepository>();

      // Infra - Services
      services.AddScoped<BdTransactionService>();
    }
  }
}
