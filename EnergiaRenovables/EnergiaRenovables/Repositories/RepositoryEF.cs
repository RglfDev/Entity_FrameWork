
using EnergiaRenovables.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EnergiaRenovables.Repositories
{
    public class RepositoryEF<T> : IRepository<T> where T : class
    {
        private readonly RenovableContext? _context; 

        private readonly DbSet<T>? _dbset; 

        public RepositoryEF(RenovableContext? context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbset.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _dbset.Where(predicate).ToListAsync();


        public async Task<IEnumerable<T>> GetAllAsync() => await _dbset.ToListAsync();


        public async Task<T?> GetByIdAsync(int id) => await _dbset.FindAsync(id);


        public async Task UpdateAsync(T entity)
        {
            _dbset.Update(entity);
            await _context.SaveChangesAsync();
        }


    }
}
