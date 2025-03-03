using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.DataAccessObjects;
using LeQuangTrungMVC.BusinessLogicLayer.Interfaces;
using LeQuangTrungMVC.ActionFilter;

namespace LeQuangTrungMVC.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }


        [StaffAndAdmin]
        public async Task<IActionResult> Index()
        {
            return View(await _tagService.GetTags());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagService.GetTag(id.Value);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        //// GET: Tags/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Tags/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("TagId,TagName,Note")] Tag tag)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        tag.TagId = Guid.NewGuid();
        //        _context.Add(tag);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(tag);
        //}

        //// GET: Tags/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tag = await _context.Tags.FindAsync(id);
        //    if (tag == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(tag);
        //}

        //// POST: Tags/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("TagId,TagName,Note")] Tag tag)
        //{
        //    if (id != tag.TagId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(tag);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TagExists(tag.TagId))
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
        //    return View(tag);
        //}

        // GET: Tags/CreateModal
        public IActionResult CreateModal()
        {
            return PartialView("_CreatePartial", new Tag());
        }

        // POST: Tags/CreateModal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModal([Bind("TagName,Note")] Tag tag)
        {
            try
            {
                await _tagService.CreateTag(tag);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return PartialView("_CreatePartial", tag);
            }

        }

        // GET: Tags/EditModal/5
        public async Task<IActionResult> EditModal(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagService.GetTag(id.Value);
            if (tag == null)
            {
                return NotFound();
            }
            return PartialView("_EditPartial", tag);
        }

        // POST: Tags/EditModal/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditModal(Guid id, [Bind("TagId,TagName,Note")] Tag tag)
        {
            if (id != tag.TagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tagService.UpdateTag(tag);
                    return Json(new { success = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    var check = await TagExists(tag.TagId);
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
            return PartialView("_EditPartial", tag);
        }

        //// GET: Tags/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tag = await _tagService.GetTag(id.Value);
        //    if (tag == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tag);
        //}

        //// POST: Tags/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    try
        //    {
        //        await _tagService.DeleteTag(id);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction(nameof(Delete), new { id = id });
        //    }
        //}

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagService.GetTag(id.Value);
            if (tag == null)
            {
                return NotFound();
            }

            // If it's a regular GET request (not AJAX), show the full delete view
            if (!Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
            {
                return View(tag);
            }

            // For AJAX requests, return a JSON response
            return Json(new { success = true, itemId = tag.TagId, itemName = tag.TagName });
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _tagService.DeleteTag(id);

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
        private async Task<bool> TagExists(Guid id)
        {
            return await _tagService.GetTag(id) != null;
        }
    }
}
