using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MagicVilla.Data;
using MagicVilla.Model;
using MagicVilla.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _db;
       
        public VillaRepository(ApplicationDbContext db){
            _db = db;
            
        }
        public async Task Create(Villa entity)
        {
            await _db.Villas.AddAsync(entity);
            await Save();
        }

        public async Task<Villa> Get(Expression<Func<Villa,bool>> filter = null)
        {
            IQueryable<Villa> query =_db.Villas;
            if(query !=null){
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Villa>> GetAll(Expression<Func<Villa,bool>> filter = null)
        {
            IQueryable<Villa> query =_db.Villas;
            if(query !=null){
                
                query = query.Where(filter);
            }
            return await query.ToListAsync();

        }

        public async Task Remove(Villa entity)
        {
            _db.Villas.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}