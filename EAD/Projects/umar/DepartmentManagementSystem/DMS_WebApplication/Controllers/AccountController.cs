using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AccountController : Controller
{
    private readonly AppDbContext _db;
    public AccountController(AppDbContext db) { _db = db; }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Role", user.Role);
            Response.Cookies.Append("UserRole", user.Role);
            return RedirectToAction("Index", user.Role);
        }
        ViewBag.Error = "Invalid";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        Response.Cookies.Delete("UserRole");
        return RedirectToAction("Login");
    }
}