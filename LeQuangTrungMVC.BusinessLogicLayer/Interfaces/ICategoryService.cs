using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.BusinessLogicLayer.Interfaces
{
    public interface ICategoryService
    {
        public Task<IList<Category>> GetCategories();

        public Task<Category?> GetCategory(Guid id);

        public Task CreateCategory(Category category);

        public Task DeleteCategory(Guid id);

        public Task UpdateCategory(Category category);
    }
}
