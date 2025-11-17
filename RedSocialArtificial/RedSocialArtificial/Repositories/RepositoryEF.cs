using Microsoft.EntityFrameworkCore;
using RedSocialArtificial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RedSocialArtificial.Repositories
{
    public class RepositoryEF<T> : IRepository<T> where T : class
    {
        private readonly AIColaboraContext? _context; //Aqui ya tenemos la base de datos

        private readonly DbSet<T>? _dbset; //A traves de esta variable vamos a poder manejar las clases Anima, Adopter o Adoption

        public RepositoryEF(AIColaboraContext? context) //Constructor de esta clase que usa el contexto de la BBDD
        {
            _context = context; //El contexto sera el contexto
            _dbset = context.Set<T>(); //A traves del contexto sacamos el tipo de entidad que le llega
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
