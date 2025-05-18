using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class DashboardController : BaseController
{
    public IActionResult Index()
    {
        if (User.IsInRole("SuperAdmin"))
        {
            return RedirectToAction("ManageRoles", "Admin");
        }
        else if (User.IsInRole("Admin"))
        {
            return RedirectToAction("Index", "Student"); // Example: Admin land on Manage Students page
        }
        else if (User.IsInRole("Clerk"))
        {
            return RedirectToAction("Index", "Exam");  // Example: Clerk land on Exam Sheets page
        }
        else
        {
            return View("AccessDenied");
        }
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View("~/Views/Account/AccessDenied.cshtml");
    }
}
