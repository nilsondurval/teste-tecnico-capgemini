using Microsoft.Extensions.Configuration;
using capgemini_api.Infra.Data.Context;
using capgemini_api.Domain.Models.Entitys;

namespace capgemini_api.Infra.Data.Repository
{
    public class ImportacaoRepository : BaseRepository<Importacao>
    {
        public ImportacaoRepository(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {

        }
    }
}