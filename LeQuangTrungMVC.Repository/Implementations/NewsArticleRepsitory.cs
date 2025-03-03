using LeQuangTrungMVC.BusinessObjects.Enums;
using LeQuangTrungMVC.BusinessObjects.Features;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.DataAccessObjects;
using LeQuangTrungMVC.Repository.Extensions;
using LeQuangTrungMVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeQuangTrungMVC.Repository.Implementations
{
    public class NewsArticleRepsitory : INewsArticleRepository
    {
        public async Task CreateNewsArticle(NewsArticle newsArticle)
        {
            await NewsArticleDAO.Instance.Create(newsArticle);
        }

        public async Task DeleteNewsArticle(NewsArticle newsArticle)
        {
            await NewsArticleDAO.Instance.Delete(newsArticle);
        }

        public async Task<NewsArticle?> GetNewsArticle(Guid id)
        {
            return await NewsArticleDAO.Instance.FindByCondition(ns => ns.NewsArticleId.Equals(id)).SingleOrDefaultAsync();
        }

        public async Task<IList<NewsArticle>> GetNewsArticles(NewsStatus? newsStatus = null)
        {
            return await NewsArticleDAO.Instance
                .FindByCondition(n => newsStatus == null || n.NewsStatus == newsStatus.Value)
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .Include(n => n.Tags)
                .ToListAsync();
        }

        public async Task<PagedList<NewsArticle>> GetNewsArticles(NewsArticleParameters parameters, NewsStatus? newsStatus = null)
        {
            var newsArticles = await NewsArticleDAO.Instance.FindByCondition(n => newsStatus == null || n.NewsStatus == newsStatus.Value)
                .SearchByContent(parameters.SearchByContent)
                .Sort(parameters.OrderBy)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .Include(n => n.Tags)
                .ToListAsync();

            var count = await NewsArticleDAO.Instance.FindAll()
                .SearchByContent(parameters.SearchByContent)
                .CountAsync();


            return new PagedList<NewsArticle>(
                newsArticles,
                count,
                parameters.PageNumber,
                parameters.PageSize);
        }

        public async Task UpdateNewsArticle(NewsArticle newsArticle)
        {
            await NewsArticleDAO.Instance.Update(newsArticle);
        }
    }
}
