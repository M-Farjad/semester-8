using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ExamManagmentSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExamManagmentSystem.Controllers
{
    [Authorize(Policy = "Permission.ManageExamSheets")]
    public class ExamController : BaseController
    {
        private readonly IConverter _pdfConverter;

        // Simulating a DB for demo — replace with actual DbContext later
        private static List<Student> _students = new List<Student>
        {
            new() { Id = 1,  RollNumber = "2021-CS(A)-01", Name = "Ali Khan",        Batch = "2021", Section = "CS(A)" },
            new() { Id = 2,  RollNumber = "2021-CS(A)-02", Name = "Sara Iqbal",      Batch = "2021", Section = "CS(A)" },
            new() { Id = 3,  RollNumber = "2021-CS(B)-01", Name = "Zain Shah",       Batch = "2021", Section = "CS(B)" },
            new() { Id = 4,  RollNumber = "2022-SE(A)-01", Name = "Ayesha Raza",     Batch = "2022", Section = "SE(A)" },
            new() { Id = 5,  RollNumber = "2023-CS(C)-01", Name = "Usman Tariq",     Batch = "2023", Section = "CS(C)" },
            new() { Id = 6,  RollNumber = "2022-SE(A)-02", Name = "Fatima Javed",    Batch = "2022", Section = "SE(A)" },
            new() { Id = 7,  RollNumber = "2022-SE(A)-03", Name = "Bilal Ahmad",     Batch = "2022", Section = "SE(A)" },
            new() { Id = 8,  RollNumber = "2022-SE(A)-02", Name = "Zainab Saeed",    Batch = "2022", Section = "SE(A)" },
            new() { Id = 9,  RollNumber = "2022-SE(A)-03", Name = "Ahmed Bashir",    Batch = "2022", Section = "SE(A)" },
            new() { Id = 10, RollNumber = "2022-SE(A)-04", Name = "Maha Farooq",     Batch = "2022", Section = "SE(A)" },
            new() { Id = 11, RollNumber = "2023-CS(A)-01", Name = "Hamza Siddiqui",  Batch = "2023", Section = "CS(A)" },
            new() { Id = 12, RollNumber = "2023-CS(A)-02", Name = "Rubab Ali",       Batch = "2023", Section = "CS(A)" },
            new() { Id = 13, RollNumber = "2023-CS(B)-01", Name = "Shahid Mehmood",  Batch = "2023", Section = "CS(B)" },
            new() { Id = 14, RollNumber = "2023-CS(B)-02", Name = "Nimra Yousaf",    Batch = "2023", Section = "CS(B)" },
            new() { Id = 15, RollNumber = "2023-CS(B)-03", Name = "Faisal Qureshi",  Batch = "2023", Section = "CS(B)" },
            new() { Id = 16, RollNumber = "2021-IT(A)-01", Name = "Hiba Aslam",      Batch = "2021", Section = "IT(A)" },
            new() { Id = 17, RollNumber = "2021-IT(A)-02", Name = "Kashif Bhatti",   Batch = "2021", Section = "IT(A)" },
            new() { Id = 18, RollNumber = "2021-CS(C)-01", Name = "Mehwish Khan",    Batch = "2021", Section = "CS(C)" },
            new() { Id = 19, RollNumber = "2021-CS(C)-02", Name = "Zohaib Anwar",    Batch = "2021", Section = "CS(C)" },
            new() { Id = 20, RollNumber = "2021-CS(C)-03", Name = "Iqra Saleem",     Batch = "2021", Section = "CS(C)" },
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
