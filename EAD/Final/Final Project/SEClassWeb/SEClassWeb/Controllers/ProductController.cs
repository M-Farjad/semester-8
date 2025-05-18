using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEClassWeb.Models;

namespace SEClassWeb.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult viewproduct()
        {
            Se21Context db = new Se21Context ();
            List<Uetuser> mylist = db.Uetusers.ToList();

            

            return View("viewproduct", mylist);
        }

        [HttpPost]
        public ActionResult saveproduct(string user_name, string user_password)
        {


            using (Se21Context db = new Se21Context())
            {
                Uetuser temp = new Uetuser { Username = user_name, Password = user_password };
                db.Uetusers.Add(temp);
                db.SaveChanges();
            }
            return RedirectToAction("viewproduct");
        }

        public ActionResult deleteuser(string rowid)
        {
            using(Se21Context db = new Se21Context())
            {
                Uetuser? temp = db.Uetusers.FirstOrDefault(x => x.Id == System.Convert.ToInt32( rowid));

                db.Uetusers.Remove(temp);
                db.SaveChanges();
            }
            return RedirectToAction("viewproduct");

        }

        [HttpPost]
        public int delete(int rowid)
        {
            using (Se21Context db = new Se21Context())
            {
                Uetuser? temp = db.Uetusers.FirstOrDefault(x => x.Id == System.Convert.ToInt32(rowid));

                db.Uetusers.Remove(temp);
                db.SaveChanges();
            }
            return 1;

        }

    }
}
