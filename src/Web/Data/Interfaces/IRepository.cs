namespace Web.Data.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken canecllationToken = default);

        public Task<IEnumerable<TEntity>?> GetListAsync(CancellationToken canecllationToken = default);

        public Task<TEntity?> AddAsync(TEntity entity, CancellationToken canecllationToken = default);

        public Task<TEntity?> DeleteAsync(Guid id, CancellationToken canecllationToken = default);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
