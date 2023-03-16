using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MagicVilla.Model;

namespace MagicVilla.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter = null);
        Task<T> GetAsync(Expression<Func<T,bool>>? filter = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync(); 
    }
}