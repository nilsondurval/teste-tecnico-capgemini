using System.Collections.Generic;
using System.Threading.Tasks;

namespace capgemini_api.Data.Base
{
  public interface IBaseData<T>
  {
    Task AddRange(List<T> entity);

    Task<List<T>> Get();

    Task<T> Get(long id);
  }
}