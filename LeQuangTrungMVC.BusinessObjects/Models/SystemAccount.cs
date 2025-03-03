using LeQuangTrungMVC.BusinessObjects.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LeQuangTrungMVC.BusinessObjects.Models
{
    public class SystemAccount
    {
        public Guid AccountId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? AccountName { get; set; }

        [Required]
        [EmailAddress]
        public string AccountEmail { get; set; } = null!;

        public AccountRole AccountRole { get; set; } = AccountRole.Lecturer;

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string AccountPassword { get; set; } = null!;

        [NotMapped]
        [Required]
        [Compare("AccountPassword")]
        public string? ConfirmPassword { get; set; }

        public virtual ICollection<NewsArticle> NewsArticleCreatedBies { get; set; } = new List<NewsArticle>();

        public virtual ICollection<NewsArticle> NewsArticleUpdatedBies { get; set; } = new List<NewsArticle>();
    }
}
