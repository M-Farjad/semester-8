using Microsoft.AspNetCore.Mvc;
using SEClassWeb.Models;
using Microsoft.AspNetCore.Http;

namespace SEClassWeb.Controllers
{
    public class dashboardController : Controller
    {
        public IActionResult praccss()
        {
            return View();
        }

        public IActionResult Login()
        {
            Console.WriteLine("Login");

            // If already logged in, redirect to homepage
            if (HttpContext.Session.GetString("user") != null)
            {
                return RedirectToAction("homepage");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(Uetuser obj)
        {
            Console.WriteLine("LoginHttpPost");

            if (ModelState.IsValid)
            {
                using (Se21Context db = new Se21Context())
                {
                    var temp = db.Uetusers.FirstOrDefault(x => x.Username == obj.Username && x.Password == obj.Password);

                    if (temp == null)
                    {
                        return RedirectToAction("Login");
                    }

                    // Set session: this will auto-expire in 30 seconds if properly configured in Startup.cs
                    HttpContext.Session.SetString("user", temp.Username);

                    return RedirectToAction("homepage");
                }
            }

            return View(obj);
        }

        public IActionResult homepage()
        {
            // Only allow access if session exists
            var username = HttpContext.Session.GetString("user");

            if (!string.IsNullOrEmpty(username))
            {
                return View("homepage", username);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            using (Se21Context db = new Se21Context())
            {
                var lst = db.Uetusers.ToList();
                return View("Index", lst);
            }
        }

        [HttpPost]
        public bool deleteuser(int pkid)
        {
            using (Se21Context db = new Se21Context())
            {
                var temp = db.Uetusers.FirstOrDefault(x => x.Id == pkid);

                if (temp == null)
                {
                    return false;
                }

                db.Uetusers.Remove(temp);
                db.SaveChanges();
                return true;
            }
        }

        public JsonResult get_uet_users()
        {
            using (Se21Context db = new Se21Context())
            {
                return Json(db.Uetusers.ToList());
            }
        }
    }
}
