using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Friendsbook.Persistence.Repositories
{
    public class Repository<T> where T : class
    {
        protected readonly FriendsbookContext _context;

        public Repository(FriendsbookContext context)
        {
            _context = context;
        }

        public async Task<EntityEntry<T>> AddAsync(T entity)
        {
            return await _context.Set<T>().AddAsync(entity).ConfigureAwait(false);
        }

        public Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToListAsync();
        }

        public ValueTask<T?> GetAsync(int id)
        {
            return _context.Set<T>().FindAsync(id);
        }

        public Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefaultAsync(predicate);
        }


        public Task<List<T>> GetAllAsync()
        {
            return _context.Set<T>().ToListAsync();
        }


        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public ValueTask<T?> GetAsync(long id)
        {
            return _context.Set<T>().FindAsync(id);
        }

    }
}
