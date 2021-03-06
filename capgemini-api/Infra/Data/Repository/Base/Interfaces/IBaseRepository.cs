﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace capgemini_api.Infra.Data.Repository.Interfaces
{
  public interface IBaseRepository<T> where T : class
  {
    IQueryable<T> Get();
    
    Task<T> GetAsync(short id);   

    Task<T> GetAsync(int id);

    Task<T> GetAsync(long id);

    Task AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entitys);

    Task UpdateAsync(T entity);

    Task UpdateRangeAsync(IEnumerable<T> entitys);
    
    Task RemoveAsync(short id);

    Task RemoveAsync(int id);

    Task RemoveAsync(long id);

    IQueryable<T> Find(Expression<Func<T, bool>> expressionWhere);
  }
}
