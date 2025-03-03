using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.BusinessLogicLayer.Interfaces
{
    public interface ITagService
    {
        public Task<IList<Tag>> GetTags();

        public Task<Tag?> GetTag(Guid id);

        public Task CreateTag(Tag tag);

        public Task DeleteTag(Guid id);

        public Task UpdateTag(Tag tag);
    }
}
