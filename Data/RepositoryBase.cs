using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Data;
using ProjectApi.Models;



namespace ProjectApi.Data
{
public interface IRepositoryBase<T> where T : BaseEntity
    {


        
        Task<T> GetbyIdAsync(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);

        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
    
         Task BulkInsert(List<T> Collections);
        
         
    }

public   class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {

        private readonly DataContext Context;
       


        public RepositoryBase(DataContext _context)
        {
            Context = _context;
        }
      

      

   

   
    

        public Task<T> GetbyIdAsync(int id) => Context.Set<T>().FindAsync(id);

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task Create(T entity)
        {
            // await Context.AddAsync(entity);
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();


        }

        public async Task BulkInsert(List<T> Collections)
        {

            await Context.Set<T>().AddRangeAsync(Collections);


            
           
            await Context.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            // In case AsNoTracking is used
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }
        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() => Context.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate) 
            => Context.Set<T>().CountAsync(predicate);


         


    
}

}