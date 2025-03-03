using LeQuangTrungMVC.BusinessLogicLayer.Interfaces;
using LeQuangTrungMVC.BusinessObjects.Configurations;
using LeQuangTrungMVC.BusinessObjects.Enums;
using LeQuangTrungMVC.BusinessObjects.ExceptionModels;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace LeQuangTrungMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly AdminAccount _adminAccount;

        public HomeController(IAccountService accountService, IOptionsSnapshot<AdminAccount> configuration)
        {
            _accountService = accountService;
            _adminAccount = configuration.Value;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("AccountId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SystemAccount systemAccount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _accountService.CreateAccount(systemAccount);

                    return RedirectToAction("Index");
                }
                catch (Exception ex) when (ex is BadRequestException)
                {
                    ViewBag.error = ex.Message;
                    return View();
                }
            }
            return View();


        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_adminAccount.Email.Equals(loginViewModel.Email) && _adminAccount.Password.Equals(loginViewModel.Password))
                    {
                        HttpContext.Session.SetString("AccountRole", AccountRole.Admin.ToString());
                        HttpContext.Session.SetString("AccountId", "Admin");
                        HttpContext.Session.SetString("AccountName", "Admin");
                        HttpContext.Session.SetString("AccountEmail", "Admin");
                    }
                    else
                    {
                        var account = await _accountService.GetAccount(loginViewModel.Email!, loginViewModel.Password!);

                        HttpContext.Session.SetString(nameof(account.AccountRole), account.AccountRole.ToString());
                        HttpContext.Session.SetString(nameof(account.AccountId), account.AccountId.ToString());
                        HttpContext.Session.SetString(nameof(account.AccountName), account.AccountName!);
                        HttpContext.Session.SetString(nameof(account.AccountEmail), account.AccountEmail!);
                    }

                    return RedirectToAction("Index");

                }
                catch (Exception ex) when (ex is NotFoundException || ex is UnauthorizedException)
                {
                    ViewBag.error = ex.Message;
                    return View("Login");
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
