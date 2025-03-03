using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IList<Category>> GetCategories();

        public Task<Category?> GetCategory(Guid id);

        public Task CreateCategory(Category category);

        public Task UpdateCategory(Category category);

        public Task DeleteCategory(Category category);
    }
}
