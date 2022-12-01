using Microsoft.EntityFrameworkCore;

namespace OceanicWorldAirService.Helpers
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private readonly DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">Generic DbContext.</param>
        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<T> Create(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> Create(IEnumerable<T> entities)
        {
            foreach (var a in entities)
            {
                _context.Set<T>().Add(a);
            }

            await _context.SaveChangesAsync();

            return entities;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> Create(params T[] entities)
        {
            return await Create(entities.AsEnumerable());
        }

        /// <inheritdoc/>
        public IQueryable<T> Read()
        {
            return _context.Set<T>();
        }

        /// <inheritdoc/>
        public IQueryable<T> ReadOnly()
        {
            return _context.Set<T>().AsNoTracking();
        }

        /// <inheritdoc/>
        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> Update(IEnumerable<T> entities)
        {
            foreach (var a in entities)
            {
                _context.Set<T>().Update(a);
            }

            await _context.SaveChangesAsync();

            return entities;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> Update(params T[] entities)
        {
            return await Update(entities.AsEnumerable());
        }

        /// <inheritdoc/>
        public async Task<T> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> Delete(IEnumerable<T> entities)
        {
            foreach (var a in entities)
            {
                _context.Set<T>().Remove(a);
            }

            await _context.SaveChangesAsync();

            return entities;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> Delete(params T[] entities)
        {
            return await Delete(entities.AsEnumerable());
        }

        /// <inheritdoc/>
        public async Task<ICollection<T>> GetAll(int limit)
        {
            return await _context.Set<T>().Take(limit).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task SaveChangesAndClear()
        {
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }

        /// <inheritdoc/>
        public void ChangeStateToAdded(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }
    }
}