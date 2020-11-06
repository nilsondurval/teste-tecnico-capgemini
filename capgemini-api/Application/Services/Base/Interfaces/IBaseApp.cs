using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace capgemini_api.Infra.Data.Services.Base.Interfaces
{
  public interface IBaseApp<T,U> where T : class where U : class
  {
    IQueryable<U> Get();
    
    Task<U> GetAsync(short id);   

    Task<U> GetAsync(int id);

    Task<U> GetAsync(long id);

    Task AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entitys);

    Task UpdateAsync(T entity);

    Task UpdateRangeAsync(IEnumerable<T> entitys);
    
    Task RemoveAsync(short id);

    Task RemoveAsync(int id);

    Task RemoveAsync(long id);
  }
}
