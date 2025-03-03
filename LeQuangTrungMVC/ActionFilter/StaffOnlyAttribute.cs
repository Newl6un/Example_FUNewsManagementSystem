using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using LeQuangTrungMVC.BusinessObjects.Enums;
using System;

namespace LeQuangTrungMVC.ActionFilter
{
    public class StaffOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var accountRole = session.GetString("AccountRole");

            // Nếu chưa đăng nhập hoặc role khác "staff"
            if (string.IsNullOrEmpty(accountRole) ||
                !accountRole.Equals(AccountRole.Staff.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
