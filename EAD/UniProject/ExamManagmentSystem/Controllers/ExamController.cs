using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ExamManagmentSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExamManagmentSystem.Controllers
{
    [Authorize(Policy = "Permission.ManageExamSheets")]
    public class ExamController : BaseController
    {
        private readonly IConverter _pdfConverter;
        private readonly ApplicationDbContext _context;

        public ExamController(IConverter pdfConverter, ApplicationDbContext context)
        {
            _pdfConverter = pdfConverter;
            _context = context;
        }

        public IActionResult Index()
        {
            // Get all batches and their sections for dropdowns
            var batches = _context.Batches
                .Include(b => b.Sections)
                .ToList();

            return View(batches); // Your view should be updated accordingly
        }

        [HttpPost]
        public IActionResult GenerateAttendanceSheet(int sectionId)
        {
            var section = _context.Sections
                .Include(s => s.Batch)
                .FirstOrDefault(s => s.Id == sectionId);

            if (section == null)
                return NotFound();

            var students = _context.Students
                .Where(s => s.SectionId == sectionId)
                .OrderBy(s => s.RollNumber)
                .ToList();

            var htmlContent = GenerateAttendanceHtml(section.Batch.Year.ToString(), section.Name, students);
            var pdfBytes = GeneratePdfFromHtml(htmlContent, $"Attendance_{section.Batch.Year}_{section.Name}.pdf");

            return File(pdfBytes, "application/pdf", $"Attendance_{section.Batch.Year}_{section.Name}.pdf");
        }

        [HttpPost]
        public IActionResult GenerateSittingPlan(int sectionId)
        {
            var section = _context.Sections
                .Include(s => s.Batch)
                .FirstOrDefault(s => s.Id == sectionId);

            if (section == null)
                return NotFound();

            var students = _context.Students
                .Where(s => s.SectionId == sectionId)
                .OrderBy(_ => Guid.NewGuid()) // Randomized
                .ToList();

            var htmlContent = GenerateSittingPlanHtml(section.Batch.Year.ToString(), section.Name, students);
            var pdfBytes = GeneratePdfFromHtml(htmlContent, $"SittingPlan_{section.Batch.Year}_{section.Name}.pdf");

            return File(pdfBytes, "application/pdf", $"SittingPlan_{section.Batch.Year}_{section.Name}.pdf");
        }

        private string GenerateAttendanceHtml(string batch, string section, List<Student> students)
        {
            var sb = new StringBuilder();
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
            var sb = new StringBuilder();
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

        private byte[] GeneratePdfFromHtml(string htmlContent, string filename)
        {
            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    DocumentTitle = filename,
                    Margins = new MarginSettings { Top = 10, Bottom = 10 }
                },
                Objects = { new ObjectSettings { HtmlContent = htmlContent } }
            };

            return _pdfConverter.Convert(doc);
        }
    }
}
