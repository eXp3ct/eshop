using Microsoft.EntityFrameworkCore;
using Web.Data.Interfaces;

namespace Web.Data.Repositories
{
    public class BaseRepository<TEntity>(IAppDbContext context, ILogger<BaseRepository<TEntity>> logger) : IRepository<TEntity> where TEntity : class
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<BaseRepository<TEntity>> _logger = logger;
        private DbSet<TEntity> Set => _context.Set<TEntity>();
        private readonly string _typeName = typeof(TEntity).Name;

        public async Task<TEntity?> AddAsync(TEntity entity, CancellationToken canecllationToken = default)
        {
            try
            {
                var entry = await Set.AddAsync(entity, canecllationToken)
                    ?? throw new Exception($"Failed to add {_typeName} to database");

                _logger.LogInformation("New {typeName} was added to database", _typeName);

                return entry.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add {typeName} to database", _typeName);

                return null;
            }
        }

        public async Task<TEntity?> DeleteAsync(Guid id, CancellationToken canecllationToken = default)
        {
            try
            {
                var entityToDelete = await GetByIdAsync(id, canecllationToken)
                    ?? throw new Exception($"{_typeName} not found in database");

                var entry = Set.Remove(entityToDelete);

                _logger.LogInformation("{typeName} was deleted from database", _typeName);

                return entry.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete {typeName} from delete", _typeName);

                return null;
            }
        }


        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken canecllationToken = default)
        {
            try
            {
                var entity = await Set.FindAsync([id, canecllationToken], cancellationToken: canecllationToken)
                    ?? throw new Exception($"Failed to find {_typeName} in database");

                if (entity is null)
                    _logger.LogWarning("{typeName} not found in database", _typeName);
                else
                    _logger.LogInformation("{typeName} found in database", _typeName);

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to find {typeName} in database", _typeName);

                return null;
            }
        }

        public async Task<IEnumerable<TEntity>?> GetListAsync(CancellationToken canecllationToken = default)
        {
            try
            {
                var list = await Set.ToListAsync(cancellationToken: canecllationToken)
                    ?? throw new Exception($"Failed to get list of {_typeName} from database");

                _logger.LogInformation("Found {count} of {typeName} in database", list.Count, _typeName);

                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to find {typeName} in database", _typeName);

                return null;
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
