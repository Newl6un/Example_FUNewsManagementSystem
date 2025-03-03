using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.DataAccessObjects;
using Microsoft.Identity.Client;
using LeQuangTrungMVC.BusinessLogicLayer.Interfaces;
using LeQuangTrungMVC.BusinessObjects.ExceptionModels;
using LeQuangTrungMVC.BusinessLogicLayer.Implementations;
using LeQuangTrungMVC.ActionFilter;

namespace LeQuangTrungMVC.Controllers
{
    public class SystemAccountsController : Controller
    {
        private readonly IAccountService _accountService;

        public SystemAccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: SystemAccounts
        [AdminOnly]
        public async Task<IActionResult> Index()
        {
            return View(await _accountService.GetAccounts());
        }

        // GET: SystemAccounts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await _accountService.GetAccountAsync(id.Value);
            if (systemAccount == null)
            {
                return NotFound();
            }

            return View(systemAccount);
        }

        //// GET: SystemAccounts/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: SystemAccounts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount systemAccount)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        systemAccount.AccountId = Guid.NewGuid();
        //        _context.Add(systemAccount);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(systemAccount);
        //}

        //// GET: SystemAccounts/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var systemAccount = await _context.SystemAccounts.FindAsync(id);
        //    if (systemAccount == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(systemAccount);
        //}

        //// POST: SystemAccounts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount systemAccount)
        //{
        //    if (id != systemAccount.AccountId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(systemAccount);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SystemAccountExists(systemAccount.AccountId))
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
        //    return View(systemAccount);
        //}

        // GET: SystemAccounts/CreateModal
        public IActionResult CreateModal()
        {
            return PartialView("_CreatePartial", new SystemAccount());
        }

        // POST: SystemAccounts/CreateModal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModal([Bind("AccountName,AccountEmail,AccountPassword,ConfirmPassword,AccountRole")] SystemAccount account)
        {
            try
            {
                await _accountService.CreateAccount(account);
                return Json(new { success = true });
            }
            catch (Exception ex) when (ex is BadRequestException)
            {
                ModelState.AddModelError("AccountEmail", ex.Message);
                return PartialView("_CreatePartial", account);
            }
        }

        // GET: SystemAccounts/EditModal/5
        public async Task<IActionResult> EditModal(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _accountService.GetAccountAsync(id.Value);
            if (account == null)
            {
                return NotFound();
            }

            // Clear the password for security
            account.AccountPassword = string.Empty;
            account.ConfirmPassword = string.Empty;

            return PartialView("_EditPartial", account);
        }

        // POST: SystemAccounts/EditModal/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditModal(Guid id, [Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword,ConfirmPassword")] SystemAccount account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            try
            {
                await _accountService.UpdateAccount(id, account);
                return Json(new { success = true });
            }
            catch (DbUpdateConcurrencyException)
            {
                var check = await SystemAccountExists(account.AccountId);
                if (!check)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // return PartialView("_EditPartial", account);
        }

        // GET: SystemAccounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await _accountService.GetAccountAsync(id.Value);
            if (systemAccount == null)
            {
                return NotFound();
            }

            return View(systemAccount);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _accountService.GetAccountAsync(id);

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

        private async Task<bool> SystemAccountExists(Guid id)
        {
            return await _accountService.GetAccountAsync(id) != null;
        }
    }
}
