using Microsoft.AspNetCore.Mvc;

namespace SEClassWeb.Controllers
{
    public class LoginController : Controller
    {

        public int sum(int num1, int num2)
        {
            return num1 + num2;
        }

        public ActionResult myfirstpage()
        {
            return View();
        }

        public ActionResult Signup()
        {

            //ViewBag.num = 20;



            return View("Signup",10);
        }
     }
}
