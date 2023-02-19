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
        Task<List<T>> GetAll(Expression<Func<T,bool>> filter = null);
        Task<T> Get(Expression<Func<T,bool>>? filter = null);
        Task Create(T entity);
        Task Remove(T entity);
        Task Save(); 
    }
}