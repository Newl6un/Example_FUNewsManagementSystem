using LeQuangTrungMVC.ActionFilter;
using LeQuangTrungMVC.BusinessLogicLayer.Interfaces;
using LeQuangTrungMVC.BusinessObjects.Enums;
using LeQuangTrungMVC.BusinessObjects.Features;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.Models.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeQuangTrungMVC.Controllers
{
    public class NewsArticlesController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly ITagService _tagService;

        public NewsArticlesController(INewsArticleService newsArticleService, ICategoryService categoryService, IAccountService accountService, ITagService tagService)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _accountService = accountService;
            _tagService = tagService;
        }


        public async Task<IActionResult> Index(NewsArticleParameters parameters)
        {
            PagedList<NewsArticle>? pagedResult = null;
            var role = HttpContext.Session.GetString("AccountRole");
            if (role == null || role.Equals(AccountRole.Lecturer.ToString()))
            {
                pagedResult = await _newsArticleService.GetNewsArticles(parameters, NewsStatus.Active);

            }
            else
            {
                pagedResult = await _newsArticleService.GetNewsArticles(parameters);
            }

            var viewModel = new NewsArticlePagingViewModel
            {
                NewsArticles = pagedResult,
                MetaData = pagedResult.MetaData
            };
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName");
            return View(viewModel);
        }
        //public async Task<IActionResult> Create()
        //{
        //    ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(NewsArticlePagingViewModel viewModel)
        //{
        //    try
        //    {
        //        await _newsArticleService.CreateNewsArticle(viewModel.NewsArticleForCreate);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {

        //        ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName", viewModel.NewsArticleForCreate.CategoryId);
        //        return View("Index");
        //    }
        //}

        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var newsArticle = await _newsArticleService.GetNewsArticle(id.Value);
        //    if (newsArticle == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName", newsArticle.CategoryId);
        //    return View(newsArticle);
        //}

        //// POST: NewsArticles/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("NewsArticleId,NewsTile,Headline,CreatedDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdatedById,ModifiedDate")] NewsArticle newsArticle)
        //{
        //    if (id != newsArticle.NewsArticleId)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        await _newsArticleService.UpdateNewsArticle(newsArticle);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName", newsArticle.CategoryId);
        //        return View(newsArticle);
        //    }
        //    return RedirectToAction(nameof(Index));

        //}

        public async Task<IActionResult> CreateModal()
        {
            ViewBag.AccountId = HttpContext.Session.GetString("AccountId");
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName");
            return PartialView("_CreatePartial", new NewsArticle());
        }

        // POST: NewsArticles/CreateModal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModal(NewsArticle newsArticle)
        {
            try
            {

                await _newsArticleService.CreateNewsArticle(newsArticle);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Failed to create news article.");
            }

            ViewBag.AccountId = HttpContext.Session.GetString("AccountId");
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName", newsArticle.CategoryId);
            return PartialView("_CreatePartial", newsArticle);
        }

        // GET: NewsArticles/EditModal/5
        public async Task<IActionResult> EditModal(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _newsArticleService.GetNewsArticle(id.Value);
            if (newsArticle == null)
            {
                return NotFound();
            }

            ViewBag.AccountId = HttpContext.Session.GetString("AccountId");
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName", newsArticle.CategoryId);
            return PartialView("_EditPartial", newsArticle);
        }

        // POST: NewsArticles/EditModal/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditModal(NewsArticle newsArticle)
        {
            try
            {
                await _newsArticleService.UpdateNewsArticle(newsArticle);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Failed to update news article.");
            }

            ViewBag.AccountId = HttpContext.Session.GetString("AccountId");
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName", newsArticle.CategoryId);
            return PartialView("_EditPartial", newsArticle);
        }

        // GET: NewsArticles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _newsArticleService.GetNewsArticle(id.Value);
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _newsArticleService.DeleteNewsArticle(id);

                // If it's an AJAX request, return JSON
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true });
                }

                // Otherwise redirect
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Failed to delete this item." });
                }

                TempData["ErrorMessage"] = "Failed to delete this item.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
