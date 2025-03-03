using LeQuangTrungMVC.BusinessObjects.Enums;
using LeQuangTrungMVC.BusinessObjects.Features;
using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.Repository.Interfaces
{
    public interface INewsArticleRepository
    {
        public Task<IList<NewsArticle>> GetNewsArticles(NewsStatus? newsStatus = null);

        public Task<PagedList<NewsArticle>> GetNewsArticles(NewsArticleParameters parameters, NewsStatus? newsStatus = null);

        public Task<NewsArticle?> GetNewsArticle(Guid id);

        public Task CreateNewsArticle(NewsArticle newsArticle);

        public Task UpdateNewsArticle(NewsArticle newsArticle);

        public Task DeleteNewsArticle(NewsArticle newsArticle);
    }
}
