﻿using HotelListing.Data;
using HotelListing.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelListing.Repository.Implementaion
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HotelDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(HotelDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes)
        {
            IQueryable<T> query = _db;
            if (includes ==null)
                foreach (var includeProperty in includes)
                    query = query.Include(includeProperty);

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            query = (query == null) ? query : query.Where(expression);
            query = (orderBy == null) ? query : orderBy(query);
            // query = (expression == null) ? query : includes.ForEach(item => query = query.Include(item)) ;
            if (expression != null)
                foreach (var includeProperty in includes)
                    query = query.Include(includeProperty);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity)
        {
           await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
           await _db.AddRangeAsync(entities);
        }


        public void Update(T entity)
        {
            _db.Attach(entity);

            _context.Entry(entity).State=EntityState.Modified; 
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _db.UpdateRange(entities);
        }

        #region
      /*  public static void ForEach<Temp>(this IEnumerable<Temp> enumeration, Action<Temp> action)
        {
            foreach (Temp item in enumeration)
            {
                action(item);
            }
        }*/
        #endregion
    }
}