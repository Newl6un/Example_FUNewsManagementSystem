namespace LeQuangTrungMVC.BusinessObjects.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? CategoryDescription { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

        public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();

        public virtual Category? ParentCategory { get; set; }
    }
}
