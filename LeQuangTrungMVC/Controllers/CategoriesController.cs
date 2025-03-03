using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.BusinessLogicLayer.Interfaces;
using LeQuangTrungMVC.ActionFilter;

namespace LeQuangTrungMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        [StaffAndAdmin]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetCategories());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategory(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //// GET: Categories/Create
        //public IActionResult Create()
        //{
        //    ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
        //    return View();
        //}

        //// POST: Categories/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,ParentCategoryId,IsActive")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        category.CategoryId = Guid.NewGuid();
        //        _context.Add(category);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
        //    return View(category);
        //}

        //// GET: Categories/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.Categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
        //    return View(category);
        //}

        //// POST: Categories/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,ParentCategoryId,IsActive")] Category category)
        //{
        //    if (id != category.CategoryId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(category);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CategoryExists(category.CategoryId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", category.ParentCategoryId);
        //    return View(category);
        //}

        // GET: Categories/CreateModal
        public async Task<IActionResult> CreateModal()
        {
            ViewBag.ParentCategoryId = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName");
            return PartialView("_CreatePartial", new Category { IsActive = true });
        }

        // POST: Categories/CreateModal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModal([Bind("CategoryName,CategoryDescription,IsActive,ParentCategoryId")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(category);
                return Json(new { success = true });
            }
            ViewBag.ParentCategoryId = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return PartialView("_CreatePartial", category);
        }

        // GET: Categories/EditModal/5
        public async Task<IActionResult> EditModal(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategory(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            var list = await _categoryService.GetCategories();
            ViewBag.ParentCategoryId = new SelectList(list.Where(e => e.CategoryId != id).ToList(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return PartialView("_EditPartial", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditModal(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,IsActive,ParentCategoryId")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategory(category);
                    return Json(new { success = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    var check = await CategoryExists(category.CategoryId);
                    if (!check)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            var list = await _categoryService.GetCategories();
            ViewBag.ParentCategoryId = new SelectList(list.Where(e => e.CategoryId != id).ToList(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return PartialView("_EditPartial", category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategory(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _categoryService.DeleteCategory(id);

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
        private async Task<bool> CategoryExists(Guid id)
        {
            return await _categoryService.GetCategory(id) != null;
        }
    }
}
