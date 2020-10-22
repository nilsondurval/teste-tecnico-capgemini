using capgemini_api.Data.DataContext;
using Microsoft.Extensions.Configuration;

namespace capgemini_api.Data.Base
{
  public abstract class BaseData
  {
    internal ApiContext _context;

    public BaseData(ApiContext context, IConfiguration configuration)
    {
      _context = context;
    }
  }
}