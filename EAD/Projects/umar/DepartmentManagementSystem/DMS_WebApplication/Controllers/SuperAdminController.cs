using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class SuperAdminController : Controller
{
    private readonly AppDbContext _db;
    public SuperAdminController(AppDbContext db) { _db = db; }

    public IActionResult Index() => View(_db.Users.Where(u => u.Role != "SuperAdmin").ToList());

    [HttpPost]
    public IActionResult Add(string username, string password, string role)
    {
        _db.Users.Add(new User { Username = username, Password = password, Role = role });
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var user = _db.Users.Find(id);
        if (user != null) { _db.Users.Remove(user); _db.SaveChanges(); }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(int id, string username, string role, string password)
    {
        var user = _db.Users.Find(id);
        if (user != null)
        {
            user.Username = username;
            user.Role = role;

            // Only update password if provided
            if (!string.IsNullOrEmpty(password))
            {
                user.Password = password; // Note: You should hash this password in production
            }

            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    // Keep your existing Update method if needed for other purposes
    [HttpPost]
    public IActionResult Update(int id, string username, string password)
    {
        var user = _db.Users.Find(id);
        if (user != null)
        {
            user.Username = username;
            user.Password = password;
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}