using LeQuangTrungMVC.BusinessLogicLayer.Interfaces;
using LeQuangTrungMVC.BusinessObjects.Constant;
using LeQuangTrungMVC.BusinessObjects.Enums;
using LeQuangTrungMVC.BusinessObjects.ExceptionModels;
using LeQuangTrungMVC.BusinessObjects.Features;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.Repository.Interfaces;

namespace LeQuangTrungMVC.BusinessLogicLayer.Implementations
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsArticleRepository;

        public NewsArticleService(INewsArticleRepository newsArticleRepository)
        {
            _newsArticleRepository = newsArticleRepository;
        }

        public async Task CreateNewsArticle(NewsArticle newsArticle)
        {
            newsArticle.CreatedDate = DateTime.Now;
            await _newsArticleRepository.CreateNewsArticle(newsArticle);
        }

        public async Task DeleteNewsArticle(Guid id)
        {
            var newsArticle = await GetNewsArticle(id);

            await _newsArticleRepository.DeleteNewsArticle(newsArticle!);
        }

        public async Task<NewsArticle?> GetNewsArticle(Guid id)
        {
            var newsArticle = await _newsArticleRepository.GetNewsArticle(id);

            if (newsArticle is null)
                throw new NotFoundException(NewsArticleErrors.NewsArticleNotFound);

            return newsArticle;
        }

        public async Task<IList<NewsArticle>> GetNewsArticles(NewsStatus? newsStatus = null)
        {
            var newsArticles = await _newsArticleRepository.GetNewsArticles(newsStatus);

            return newsArticles;
        }

        public async Task<PagedList<NewsArticle>> GetNewsArticles(NewsArticleParameters parameters, NewsStatus? newsStatus = null)
        {
            var newsArticles = await _newsArticleRepository.GetNewsArticles(parameters, newsStatus);

            return newsArticles;
        }

        public async Task UpdateNewsArticle(NewsArticle newsArticle)
        {
            await _newsArticleRepository.UpdateNewsArticle(newsArticle);
        }
    }
}
