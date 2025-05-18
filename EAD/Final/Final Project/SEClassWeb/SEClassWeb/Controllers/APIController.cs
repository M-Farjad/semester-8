using Microsoft.AspNetCore.Mvc;
using SEClassWeb.Models;

namespace SEClassWeb.Controllers
{
    public class APIController : Controller
    {
        public JsonResult getdata()
        {
            try
            {
                using (Se21Context db = new Se21Context())
                {
                    var list = db.Uetusers.ToList();
                    return Json(list);
                }
            }
            catch(Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult create(Uetuser model)
        {
            try{
                using (Se21Context db = new Se21Context())
                {
                    db.Uetusers.Add(model);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch(Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult update(Uetuser model)
        {
            try{
                using (Se21Context db = new Se21Context())
                {
                    var user = db.Uetusers.FirstOrDefault(x => x.Id == model.Id);
                    if (user == null) return NotFound();

                    user.Username = model.Username;
                    user.Password = model.Password;
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult delete(int id)
        {
            try {
                using (Se21Context db = new Se21Context())
                {
                    var user = db.Uetusers.FirstOrDefault(x => x.Id == id);
                    if (user == null) return NotFound();

                    db.Uetusers.Remove(user);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch(Exception ex)
            {
                return Json(new { status = "error", message = ex.Message });
            }
        }
    }
}
