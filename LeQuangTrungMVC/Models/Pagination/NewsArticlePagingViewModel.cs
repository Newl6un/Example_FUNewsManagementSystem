using LeQuangTrungMVC.BusinessObjects.Features;
using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.Models.Pagination
{
    public class NewsArticlePagingViewModel
    {
        public IEnumerable<NewsArticle> NewsArticles { get; set; }
        public MetaData MetaData { get; set; }

        public NewsArticleParameters NewsArticleParameters { get; set; } = new NewsArticleParameters();

        public NewsArticle NewsArticleForCreate { get; set; }
    }
}
