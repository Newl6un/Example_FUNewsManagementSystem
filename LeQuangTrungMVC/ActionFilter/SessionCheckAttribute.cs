using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LeQuangTrungMVC.ActionFilter
{
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            // Kiểm tra các trường trong session
            if (string.IsNullOrEmpty(session.GetString("AccountRole")) ||
                string.IsNullOrEmpty(session.GetString("AccountId")) ||
                string.IsNullOrEmpty(session.GetString("AccountName")) ||
                string.IsNullOrEmpty(session.GetString("AccountEmail")))
            {
                // Chuyển hướng về trang Login nếu không có thông tin session cần thiết
                context.Result = new RedirectToActionResult("Login", "Home", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
