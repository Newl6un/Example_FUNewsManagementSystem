using LeQuangTrungMVC.BusinessLogicLayer.Interfaces;
using LeQuangTrungMVC.BusinessObjects.Constant;
using LeQuangTrungMVC.BusinessObjects.ExceptionModels;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.Repository.Interfaces;

namespace LeQuangTrungMVC.BusinessLogicLayer.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task CreateTag(Tag tag)
        {
            await _tagRepository.CreateTag(tag);
        }

        public async Task DeleteTag(Guid id)
        {
            var tag = await GetTag(id);

            await _tagRepository.DeleteTag(tag!);
        }

        public async Task<Tag?> GetTag(Guid id)
        {
            var tag = await _tagRepository.GetTag(id);

            if (tag is null)
                throw new NotFoundException(TagErrors.TagNotFound);

            return tag;
        }

        public async Task<IList<Tag>> GetTags()
        {
            var tags = await _tagRepository.GetTags();

            return tags;
        }

        public async Task UpdateTag(Tag tag)
        {
            await _tagRepository.UpdateTag(tag);
        }
    }
}
