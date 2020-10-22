using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using capgemini_api.Data.Base;
using capgemini_api.Data.DataContext;
using capgemini_api.Models.Entitys;
using System.Linq;

namespace capgemini_api.Data
{
    public class ImportacaoData : BaseData, IBaseData<Importacao>
    {
        public ImportacaoData(ApiContext context, IConfiguration configuration) : base(context, configuration)
        {

        }

        public async Task AddRange(List<Importacao> importacoes)
        {
            await _context.Importacoes.AddRangeAsync(importacoes);

            await _context.SaveChangesAsync(); ;
        }

        public Task<List<Importacao>> Get()
        {
            return _context.Importacoes.OrderBy(i => i.DataEntrega).ToListAsync();
        }

        public Task<Importacao> Get(long id)
        {
            return _context.Importacoes.SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}