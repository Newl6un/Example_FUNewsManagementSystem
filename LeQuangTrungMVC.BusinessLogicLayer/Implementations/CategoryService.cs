using LeQuangTrungMVC.BusinessLogicLayer.Interfaces;
using LeQuangTrungMVC.BusinessObjects.Constant;
using LeQuangTrungMVC.BusinessObjects.ExceptionModels;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.Repository.Interfaces;

namespace LeQuangTrungMVC.BusinessLogicLayer.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateCategory(Category category)
        {
            await _categoryRepository.CreateCategory(category!);
        }

        public async Task DeleteCategory(Guid id)
        {
            var category = await GetCategory(id);

            await _categoryRepository.DeleteCategory(category!);
        }

        public async Task<IList<Category>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();

            return categories;
        }

        public async Task<Category?> GetCategory(Guid id)
        {
            var category = await _categoryRepository.GetCategory(id);

            if (category is null)
                throw new NotFoundException(CategoryErrors.CategoryNotFound);

            return category;
        }

        public async Task UpdateCategory(Category category)
        {
            await _categoryRepository.UpdateCategory(category);
        }
    }
}
