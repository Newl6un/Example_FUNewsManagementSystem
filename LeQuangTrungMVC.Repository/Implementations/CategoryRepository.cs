using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.DataAccessObjects;
using LeQuangTrungMVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeQuangTrungMVC.Repository.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task CreateCategory(Category category)
        {
            await CategoryDAO.Instance.Create(category);
        }

        public async Task DeleteCategory(Category category)
        {
            await CategoryDAO.Instance.Delete(category);
        }

        public async Task<IList<Category>> GetCategories()
        {
            return await CategoryDAO.Instance.FindAll().Include(c => c.ParentCategory).ToListAsync();
        }

        public async Task<Category?> GetCategory(Guid id)
        {
            return await CategoryDAO.Instance.FindByCondition(c => c.CategoryId.Equals(id)).Include(c => c.ParentCategory).SingleOrDefaultAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            await CategoryDAO.Instance.Update(category);
        }
    }
}
