namespace LeQuangTrungMVC.BusinessObjects.Models
{
    public class Tag
    {
        public Guid TagId { get; set; }

        public string TagName { get; set; } = null!;

        public string? Note { get; set; }

        public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
    }
}
