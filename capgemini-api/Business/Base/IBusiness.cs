using System.Collections.Generic;
using System.Threading.Tasks;

namespace capgemini_api.Business.Base
{
  public interface IBusiness<T>
  {
    Task AddRange(List<T> entity);

    Task<List<T>> Get();

    Task<T> Get(long id);
  }
}