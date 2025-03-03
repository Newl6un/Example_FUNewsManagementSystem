//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using LeQuangTrungMVC.BusinessObjects.Models;
//using LeQuangTrungMVC.DataAccessObjects;

//namespace LeQuangTrungMVC.Views.NewsArticles
//{
//    public class NewsArticlesController : Controller
//    {
//        private readonly FunewsManagementContext _context;

//        public NewsArticlesController(FunewsManagementContext context)
//        {
//            _context = context;
//        }

//        // GET: NewsArticles
//        public async Task<IActionResult> Index()
//        {
//            var funewsManagementContext = _context.NewsArticles.Include(n => n.Category).Include(n => n.CreatedBy).Include(n => n.UpdatedBy);
//            return View(await funewsManagementContext.ToListAsync());
//        }

//        // GET: NewsArticles/Details/5
//        public async Task<IActionResult> Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var newsArticle = await _context.NewsArticles
//                .Include(n => n.Category)
//                .Include(n => n.CreatedBy)
//                .Include(n => n.UpdatedBy)
//                .FirstOrDefaultAsync(m => m.NewsArticleId == id);
//            if (newsArticle == null)
//            {
//                return NotFound();
//            }

//            return View(newsArticle);
//        }

//        // GET: NewsArticles/Create
//        public IActionResult Create()
//        {
//            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
//            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountEmail");
//            ViewData["UpdatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountEmail");
//            return View();
//        }

//        // POST: NewsArticles/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("NewsArticleId,NewsTile,Headline,CreatedDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdatedById,ModifiedDate")] NewsArticle newsArticle)
//        {
//            if (ModelState.IsValid)
//            {
//                newsArticle.NewsArticleId = Guid.NewGuid();
//                _context.Add(newsArticle);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", newsArticle.CategoryId);
//            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountEmail", newsArticle.CreatedById);
//            ViewData["UpdatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountEmail", newsArticle.UpdatedById);
//            return View(newsArticle);
//        }

//        // GET: NewsArticles/Edit/5
//        public async Task<IActionResult> Edit(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var newsArticle = await _context.NewsArticles.FindAsync(id);
//            if (newsArticle == null)
//            {
//                return NotFound();
//            }
//            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", newsArticle.CategoryId);
//            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountEmail", newsArticle.CreatedById);
//            ViewData["UpdatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountEmail", newsArticle.UpdatedById);
//            return View(newsArticle);
//        }

//        // POST: NewsArticles/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(Guid id, [Bind("NewsArticleId,NewsTile,Headline,CreatedDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdatedById,ModifiedDate")] NewsArticle newsArticle)
//        {
//            if (id != newsArticle.NewsArticleId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(newsArticle);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!NewsArticleExists(newsArticle.NewsArticleId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", newsArticle.CategoryId);
//            ViewData["CreatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountEmail", newsArticle.CreatedById);
//            ViewData["UpdatedById"] = new SelectList(_context.SystemAccounts, "AccountId", "AccountEmail", newsArticle.UpdatedById);
//            return View(newsArticle);
//        }

//        // GET: NewsArticles/Delete/5
//        public async Task<IActionResult> Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var newsArticle = await _context.NewsArticles
//                .Include(n => n.Category)
//                .Include(n => n.CreatedBy)
//                .Include(n => n.UpdatedBy)
//                .FirstOrDefaultAsync(m => m.NewsArticleId == id);
//            if (newsArticle == null)
//            {
//                return NotFound();
//            }

//            return View(newsArticle);
//        }

//        // POST: NewsArticles/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(Guid id)
//        {
//            var newsArticle = await _context.NewsArticles.FindAsync(id);
//            if (newsArticle != null)
//            {
//                _context.NewsArticles.Remove(newsArticle);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool NewsArticleExists(Guid id)
//        {
//            return _context.NewsArticles.Any(e => e.NewsArticleId == id);
//        }
//    }
//}
