using Microsoft.AspNetCore.Mvc;

namespace ExamSittingSystem.Services
{
    public class PdfService : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
