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
            var sections = _context.Sections.Include(s => s.Batch).ToList();
            return View(sections);
        }

        public IActionResult Create()
        {
            ViewBag.Batches = _context.Batches.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(string batchYear, string sectionName, int? existingBatchId)
        {
            Batch batch = null;

            if (existingBatchId.HasValue)
            {
                batch = _context.Batches.Find(existingBatchId.Value);
            }
            else if (!string.IsNullOrWhiteSpace(batchYear))
            {
                batch = new Batch { Year = batchYear };
                _context.Batches.Add(batch);
                _context.SaveChanges();
            }

            if (batch != null && !string.IsNullOrWhiteSpace(sectionName))
            {
                var section = new Section { Name = sectionName, BatchId = batch.Id };
                _context.Sections.Add(section);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
