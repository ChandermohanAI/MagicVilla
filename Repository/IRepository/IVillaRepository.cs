using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MagicVilla.Model;

namespace MagicVilla.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task GetAll(Expression<Func<Villa,bool>> filter = null);
        Task Get(Expression<Func<Villa,bool>> filter = null);
        Task Create(Villa entity);
        Task Remove(Villa entity);
        Task Save(); 
    }
}