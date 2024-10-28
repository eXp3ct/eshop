namespace Web.Services.Interfaces
{
    public interface ICategoriesAdminPanel
    {
        public Task<bool> CreateCategory(string categoryName, Guid parentCategoryId, CancellationToken cancellationToken);
        public Task<bool> EditCategory(Guid id, string newName, CancellationToken cancellationToken);
        public Task<bool> DeleteCategory(Guid categoryId, CancellationToken cancellationToken);

    }
}
