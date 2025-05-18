using Microsoft.AspNetCore.Mvc;
using SEClassWeb.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Eventing.Reader;
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
            if(HttpContext.Session.GetString("user")!=null)
            {
                return RedirectToAction("homepage");
            }
            else
            {
                return View();

            }


        }
        public IActionResult homepage()
        {
            if(HttpContext.Session.GetString("user") != null)
            {

                return View("homepage", HttpContext.Session.GetString("user"));
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public IActionResult Login(Uetuser obj)
        {
            if (ModelState.IsValid)
            {
                Uetuser? temp;
                using (Se21Context db = new Se21Context())
                {
                    temp = db.Uetusers.FirstOrDefault(x => x.Username == obj.Username && x.Password == obj.Password);
                }

                if (temp == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    HttpContext.Session.SetString("user", temp.Username);


                    return RedirectToAction("homepage");
                }
            }
            else
            {
                return View(obj);
            }
            
        }


        public IActionResult Index()
        {
            List<Uetuser>? lst;
            using(Se21Context db = new Se21Context())
            {
                lst = db.Uetusers.ToList();

            }
            return View("Index",lst);
        }


        [HttpPost]
        public bool deleteuser(int pkid)
        {
            using(Se21Context db = new Se21Context())
            {
                Uetuser? temp = db.Uetusers.FirstOrDefault(x => x.Id == pkid);
                if (temp == null) 
                {
                    return false;
                }
                else
                {
                    db.Uetusers.Remove(temp);
                    db.SaveChanges();
                    return true;
                }
                 
            }
        }

        public JsonResult get_uet_users()
        {
            Se21Context db = new Se21Context();
            return Json(db.Uetusers.ToList());
        }
    }
}
