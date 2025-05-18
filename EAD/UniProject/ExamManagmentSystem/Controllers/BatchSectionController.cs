using ExamManagmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Section = ExamManagmentSystem.Models.Section;

namespace ExamManagmentSystem.Controllers
{
    [Authorize(Policy = "Permission.AddBatch")]
    public class BatchSectionController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public BatchSectionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sections = _context.Sections
                .Include(s => s.Batch)
                .OrderBy(s => s.Batch.Year)
                .ThenBy(s => s.Name)
                .ToList();

            return View(sections);
        }

        public IActionResult Create()
        {
            ViewBag.Batches = _context.Batches.OrderByDescending(b => b.Year).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string batchYear, string sectionName, int? existingBatchId)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                ModelState.AddModelError("sectionName", "Section name is required.");
            }

            Batch batch = null;

            if (existingBatchId.HasValue)
            {
                batch = _context.Batches.Find(existingBatchId.Value);
                if (batch == null)
                {
                    ModelState.AddModelError("existingBatchId", "Selected batch does not exist.");
                }
            }
            else if (int.TryParse(batchYear, out int parsedYear))
            {
                batch = new Batch { Year = parsedYear };
                _context.Batches.Add(batch);
                _context.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("batchYear", "Invalid batch year. It must be a number.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Batches = _context.Batches.OrderByDescending(b => b.Year).ToList();
                return View();
            }

            if (batch != null)
            {
                var sectionExists = _context.Sections
                    .Any(s => s.Name == sectionName && s.BatchId == batch.Id);

                if (!sectionExists)
                {
                    var section = new Section
                    {
                        Name = sectionName,
                        BatchId = batch.Id
                    };

                    _context.Sections.Add(section);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
