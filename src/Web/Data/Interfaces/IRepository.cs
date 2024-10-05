namespace Web.Data.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        public Task<TEntity?> GetByIdAsync(
            Guid id,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
            CancellationToken canecllationToken = default);

        public Task<IEnumerable<TEntity>?> GetListAsync(
            Func<IQueryable<TEntity>,
            IQueryable<TEntity>>? include = null,
            CancellationToken canecllationToken = default);

        public Task<TEntity?> AddAsync(TEntity entity, CancellationToken canecllationToken = default);

        public Task<TEntity?> DeleteAsync(Guid id, CancellationToken canecllationToken = default);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
