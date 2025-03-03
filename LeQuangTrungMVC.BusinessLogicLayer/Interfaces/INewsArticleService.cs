using LeQuangTrungMVC.BusinessObjects.Enums;
using LeQuangTrungMVC.BusinessObjects.Features;
using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.BusinessLogicLayer.Interfaces
{
    public interface INewsArticleService
    {
        public Task<PagedList<NewsArticle>> GetNewsArticles(NewsArticleParameters parameters, NewsStatus? newsStatus = null);

        public Task<IList<NewsArticle>> GetNewsArticles(NewsStatus? newsStatus = null);

        public Task<NewsArticle?> GetNewsArticle(Guid id);

        public Task CreateNewsArticle(NewsArticle newsArticle);

        public Task DeleteNewsArticle(Guid id);

        public Task UpdateNewsArticle(NewsArticle newsArticle);
    }
}
