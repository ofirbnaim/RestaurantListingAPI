﻿using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantListingAPI.Reposetories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null,
                              Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        Task<T> Get(Expression<Func<T, bool>> expression = null,
                           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
                           

        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        Task DeleteById(int id);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);

    }
}
