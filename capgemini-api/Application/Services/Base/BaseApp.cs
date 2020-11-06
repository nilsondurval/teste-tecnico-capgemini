using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using capgemini_api.Infra.Data.Services.Base.Interfaces;
using capgemini_api.Domain.Base.Interfaces;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace capgemini_api.Infra.Data.Services.Base
{
  public class BaseApp<T,U> : IBaseApp<T,U> where T : class where U : class
  {
    private readonly IMapper _mapper;
    
    public IBaseDomain<T> _domain { get; set; }

    public BaseApp(IBaseDomain<T> domain, IMapper mapper)
    {
      _mapper = mapper;
      _domain = domain;
    }

    public IQueryable<U> Get()
    {
      return _domain.Get().ProjectTo<U>(_mapper.ConfigurationProvider);;
    }

    public async Task<U> GetAsync(short id)
    {
      var entity = await _domain.GetAsync(id);

      return _mapper.Map<U>(entity);
    }

    public async Task<U> GetAsync(int id)
    {
      var entity = await _domain.GetAsync(id);

      return _mapper.Map<U>(entity);
    }

    public async Task<U> GetAsync(long id)
    {
      var entity = await _domain.GetAsync(id);

      return _mapper.Map<U>(entity);
    }

    public async Task AddAsync(T entity)
    {
      await _domain.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entitys)
    {
      await _domain.AddRangeAsync(entitys);
    }

    public async Task UpdateAsync(T entity)
    {
      await _domain.UpdateAsync(entity);
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entitys)
    {
      await _domain.UpdateRangeAsync(entitys);
    }

    public async Task RemoveAsync(short id)
    {
      await _domain.RemoveAsync(id);
    }

    public async Task RemoveAsync(int id)
    {
      await _domain.RemoveAsync(id);
    }

    public async Task RemoveAsync(long id)
    {
      await _domain.RemoveAsync(id);
    }
  }
}
