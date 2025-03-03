using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.Repository.Utilities;
using System.Linq.Dynamic.Core;

namespace LeQuangTrungMVC.Repository.Extensions
{
    public static class NewsArticleRepsitoryExtension
    {
        public static IQueryable<NewsArticle> Sort(this IQueryable<NewsArticle> newsArticles, string? orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString)) return newsArticles.OrderBy(e => e.CreatedDate);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<NewsArticle>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return newsArticles.OrderBy(e => e.CreatedDate);

            return newsArticles.OrderBy(orderQuery);
        }

        public static IQueryable<NewsArticle> SearchByContent(this IQueryable<NewsArticle> newsArticles, string? content)
        {
            if (string.IsNullOrWhiteSpace(content)) return newsArticles;

            var lowercase = content.ToLower().Trim();

            return newsArticles.Where(e => e.NewsContent.Contains(lowercase));
        }
    }
}
