using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ExamManagmentSystem.Models;

namespace ExamManagmentSystem.Controllers
{
    public class ExamController : Controller
    {
        private readonly IConverter _pdfConverter;

        // Simulating a DB for demo — replace with actual DbContext later
        private static List<Student> _students = new List<Student>
        {
            new() { Id = 1, RollNumber = "2021-CS(A)-01", Name = "Ali Khan", Batch = "2021", Section = "CS(A)" },
            new() { Id = 2, RollNumber = "2021-CS(A)-02", Name = "Sara Iqbal", Batch = "2021", Section = "CS(A)" },
            new() { Id = 3, RollNumber = "2021-CS(B)-01", Name = "Zain Shah", Batch = "2021", Section = "CS(B)" },
            new() { Id = 4, RollNumber = "2022-SE(B)-01", Name = "Ayesha Raza", Batch = "2022", Section = "SE(B)" },
            new() { Id = 5, RollNumber = "2023-CS(C)-01", Name = "Usman Tariq", Batch = "2023", Section = "CS(C)" },
        };

        public ExamController(IConverter pdfConverter)
        {
            _pdfConverter = pdfConverter;
        }

        public IActionResult Index()
        {
            ViewBag.Batches = _students.Select(s => s.Batch).Distinct().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult GenerateAttendanceSheet(string batch, string section)
        {
            var students = _students
                .Where(s => s.Batch == batch && s.Section == section)
                .OrderBy(s => s.RollNumber)
                .ToList();

            var htmlContent = GenerateAttendanceHtml(batch, section, students);
            var pdfBytes = GeneratePdfFromHtml(htmlContent, $"Attendance_{batch}_{section}.pdf");

            return File(pdfBytes, "application/pdf", $"Attendance_{batch}_{section}.pdf");
        }

        [HttpPost]
        public IActionResult GenerateSittingPlan(string batch, string section)
        {
            var students = _students
                .Where(s => s.Batch == batch && s.Section == section)
                .OrderBy(_ => Guid.NewGuid()) // Randomize for sitting plan
                .ToList();

            var htmlContent = GenerateSittingPlanHtml(batch, section, students);
            var pdfBytes = GeneratePdfFromHtml(htmlContent, $"SittingPlan_{batch}_{section}.pdf");

            return File(pdfBytes, "application/pdf", $"SittingPlan_{batch}_{section}.pdf");
        }

        private string GenerateAttendanceHtml(string batch, string section, List<Student> students)
        {
            StringBuilder sb = new();
            sb.Append($"<h2 style='text-align:center;'>Attendance Sheet<br/>Batch {batch} - Section {section}</h2>");
            sb.Append("<table border='1' cellpadding='6' cellspacing='0' style='width:100%; border-collapse:collapse;'>");
            sb.Append("<thead><tr><th>Roll Number</th><th>Name</th><th>Signature</th></tr></thead><tbody>");

            foreach (var student in students)
            {
                sb.Append($"<tr><td>{student.RollNumber}</td><td>{student.Name}</td><td></td></tr>");
            }

            sb.Append("</tbody></table>");
            return sb.ToString();
        }

        private string GenerateSittingPlanHtml(string batch, string section, List<Student> students)
        {
            StringBuilder sb = new();
            sb.Append($"<h2 style='text-align:center;'>Sitting Plan<br/>Batch {batch} - Section {section}</h2>");
            sb.Append("<table border='1' cellpadding='6' cellspacing='0' style='width:100%; border-collapse:collapse;'>");
            sb.Append("<thead><tr><th>Seat #</th><th>Roll Number</th><th>Name</th></tr></thead><tbody>");

            int seat = 1;
            foreach (var student in students)
            {
                sb.Append($"<tr><td>{seat++}</td><td>{student.RollNumber}</td><td>{student.Name}</td></tr>");
            }

            sb.Append("</tbody></table>");
            return sb.ToString();
        }

        private byte[] GeneratePdfFromHtml(string html, string title)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    DocumentTitle = title
                },
                Objects = {
                    new ObjectSettings
                    {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            return _pdfConverter.Convert(doc);
        }

        [HttpGet]
        public JsonResult GetSections(string batch)
        {
            var sections = _students
                .Where(s => s.Batch == batch)
                .Select(s => s.Section)
                .Distinct()
                .ToList();

            return Json(sections);
        }
    }
}
