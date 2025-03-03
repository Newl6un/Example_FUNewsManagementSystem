using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.Repository.Interfaces
{
    public interface ITagRepository
    {
        public Task<IList<Tag>> GetTags();

        public Task<Tag?> GetTag(Guid id);

        public Task CreateTag(Tag tag);

        public Task UpdateTag(Tag tag);

        public Task DeleteTag(Tag tag);
    }
}
