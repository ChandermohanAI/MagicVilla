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
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;
       
        public VillaRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
            
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}