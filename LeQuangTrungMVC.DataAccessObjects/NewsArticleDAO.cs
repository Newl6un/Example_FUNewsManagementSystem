using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.DataAccessObjects
{
    public class NewsArticleDAO : GenericDAO<NewsArticle>
    {
        private static NewsArticleDAO? _instance;

        public static NewsArticleDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NewsArticleDAO();

                return _instance;
            }
        }

        public NewsArticleDAO() : base(new()) { }
    }
}
