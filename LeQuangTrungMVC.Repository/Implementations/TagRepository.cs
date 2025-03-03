using Azure;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.DataAccessObjects;
using LeQuangTrungMVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeQuangTrungMVC.Repository.Implementations
{
    public class TagRepository : ITagRepository
    {
        public async Task CreateTag(Tag tag)
        {
            await TagDAO.Instance.Create(tag);
        }

        public async Task DeleteTag(Tag tag)
        {
            await TagDAO.Instance.Delete(tag);
        }

        public Task<Tag?> GetTag(Guid id)
        {
            return TagDAO.Instance.FindByCondition(e => e.TagId.Equals(id)).SingleOrDefaultAsync();
        }

        public async Task<IList<Tag>> GetTags()
        {
            return await TagDAO.Instance.FindAll().ToListAsync();
        }

        public async Task UpdateTag(Tag tag)
        {
            await TagDAO.Instance.Update(tag);
        }
    }
}
