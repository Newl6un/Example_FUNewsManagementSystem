using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using LeQuangTrungMVC.BusinessObjects.Enums;

namespace LeQuangTrungMVC.ActionFilter
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var accountRole = session.GetString("AccountRole");

            // Nếu chưa đăng nhập hoặc role không phải "admin"
            if (string.IsNullOrEmpty(accountRole) ||
                !accountRole.Equals(AccountRole.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }

}
