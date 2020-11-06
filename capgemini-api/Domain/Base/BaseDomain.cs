using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using capgemini_api.Infra.Data.Repository.Interfaces;
using capgemini_api.Domain.Base.Interfaces;

namespace capgemini_api.Domain.Base
{
  public class BaseDomain<T> : IBaseDomain<T> where T : class
  {
    public IBaseRepository<T> _repository { get; set; }

    public BaseDomain(IBaseRepository<T> repository)
    {
      _repository = repository;
    }

    public IQueryable<T> Get()
    {
      return _repository.Get();
    }

    public Task<T> GetAsync(short id)
    {
      return _repository.GetAsync(id);
    }

    public Task<T> GetAsync(int id)
    {
      return _repository.GetAsync(id);
    }

    public Task<T> GetAsync(long id)
    {
      return _repository.GetAsync(id);
    }

    public async Task AddAsync(T entity)
    {
      await _repository.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entitys)
    {
      await _repository.AddRangeAsync(entitys);
    }

    public async Task UpdateAsync(T entity)
    {
      await _repository.UpdateAsync(entity);
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entitys)
    {
      await _repository.UpdateRangeAsync(entitys);
    }

    public async Task RemoveAsync(short id)
    {
      await _repository.RemoveAsync(id);
    }

    public async Task RemoveAsync(int id)
    {
      await _repository.RemoveAsync(id);
    }

    public async Task RemoveAsync(long id)
    {
      await _repository.RemoveAsync(id);
    }
  }
}
