using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (User.Identity.IsAuthenticated && string.IsNullOrEmpty(HttpContext.Session.GetString("LoggedIn")))
        {
            context.Result = RedirectToAction("Logout", "Account");
            return;
        }

        base.OnActionExecuting(context);
    }
}
