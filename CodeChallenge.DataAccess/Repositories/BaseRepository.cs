using CodeChallenge.DataAccess.Context;
using CodeChallenge.DataAccess.Entities;
using CodeChallenge.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(DataContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
