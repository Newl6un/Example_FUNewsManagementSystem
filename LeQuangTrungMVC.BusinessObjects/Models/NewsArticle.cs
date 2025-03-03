using LeQuangTrungMVC.BusinessObjects.Enums;

namespace LeQuangTrungMVC.BusinessObjects.Models
{
    public class NewsArticle
    {
        public Guid NewsArticleId { get; set; }

        public string NewsTile { get; set; } = null!;

        public string Headline { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string NewsContent { get; set; } = null!;

        public string? NewsSource { get; set; }

        public Guid CategoryId { get; set; }

        public NewsStatus NewsStatus { get; set; }

        public Guid CreatedById { get; set; }

        public Guid? UpdatedById { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual SystemAccount CreatedBy { get; set; } = null!;

        public virtual SystemAccount? UpdatedBy { get; set; }

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
