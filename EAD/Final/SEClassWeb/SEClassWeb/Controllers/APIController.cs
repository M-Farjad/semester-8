using Microsoft.AspNetCore.Mvc;
using SEClassWeb.Models;
namespace SEClassWeb.Controllers
{
    public class APIController : Controller
    {
        public JsonResult getdata()
        {
            List<Uetuser> ? temp = null;

                using (Se21Context db = new Se21Context())
                {
                    temp = db.Uetusers.ToList();

                }
                
                return Json(temp);
        

        }

        public ActionResult Index()
        {

            return View();
        }



    }
}
