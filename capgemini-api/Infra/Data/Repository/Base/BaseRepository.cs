using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using capgemini_api.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using capgemini_api.Infra.Data.Repository.Interfaces;

namespace capgemini_api.Infra.Data.Repository
{
  public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
  {
    protected readonly ApiContext _ApiContext;

    public BaseRepository(ApiContext dbContext, IConfiguration configuration)
    {
      _ApiContext = dbContext;
    }

    public IQueryable<T> Get()
    {
      return _ApiContext.Set<T>().AsNoTracking();
    }

    public async Task<T> GetAsync(short id)
    {
      var entity = await _ApiContext.Set<T>().FindAsync(id);
      if (entity != null)
      {
        _ApiContext.Entry(entity).State = EntityState.Detached;    
      }
      return entity;
    }

    public async Task<T> GetAsync(int id)
    {
      var entity = await _ApiContext.Set<T>().FindAsync(id);
      if (entity != null)
      {
        _ApiContext.Entry(entity).State = EntityState.Detached;    
      }
      return entity;
    }

    public async Task<T> GetAsync(long id)
    {
      var entity = await _ApiContext.Set<T>().FindAsync(id);
      if (entity != null)
      {
        _ApiContext.Entry(entity).State = EntityState.Detached;    
      }
      return entity;
    }

    public async Task AddAsync(T entity)
    {
      await _ApiContext.Set<T>().AddAsync(entity);
      await _ApiContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<T> objs)
    {
      await _ApiContext.Set<T>().AddRangeAsync(objs);
      await _ApiContext.SaveChangesAsync();
    }    

    public async Task UpdateAsync(T entity)
    {
      _ApiContext.Set<T>().Update(entity);
      await _ApiContext.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
      _ApiContext.Set<T>().UpdateRange(entities);
      await _ApiContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
      var entity = await GetAsync(id);
      _ApiContext.Set<T>().Remove(entity);
      await _ApiContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(short id)
    {
      var entity = await GetAsync(id);
      _ApiContext.Set<T>().Remove(entity);
      await _ApiContext.SaveChangesAsync();
    }  
    
    public async Task RemoveAsync(long id)
    {
      var entity = await GetAsync(id);
      _ApiContext.Set<T>().Remove(entity);
      await _ApiContext.SaveChangesAsync();
    }    

    public IQueryable<T> Find(Expression<Func<T, bool>> expressionWhere)
    {
      return _ApiContext.Set<T>().Where(expressionWhere).AsNoTracking();
    }

    public async Task SaveChangesAsync()
    {
      await _ApiContext.SaveChangesAsync();
    }

    public void Dispose()
    {
      _ApiContext.Dispose();
    }
  }
}
