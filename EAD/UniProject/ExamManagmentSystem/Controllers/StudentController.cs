using ExamManagmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamManagmentSystem.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    public class StudentController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index action to list students with filter options
        public IActionResult Index(int? batchId, int? sectionId, string roll)
        {
            var students = _context.Students
                .Include(s => s.Section)
                .ThenInclude(sec => sec.Batch)
                .AsQueryable();

            // Filtering based on BatchId, SectionId, and Roll Number
            if (batchId.HasValue)
                students = students.Where(s => s.Section.BatchId == batchId);

            if (sectionId.HasValue)
                students = students.Where(s => s.SectionId == sectionId);

            if (!string.IsNullOrWhiteSpace(roll))
                students = students.Where(s => s.RollNumber.Contains(roll));

            // Passing Batches and Sections to the View
            ViewBag.Batches = _context.Batches.Include(b => b.Sections).ToList();
            ViewBag.BatchId = batchId;
            ViewBag.SectionId = sectionId;
            ViewBag.Roll = roll;

            return View(students.ToList());
        }

        // Create action to show the student creation form
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var batches = await _context.Batches
                .Include(b => b.Sections)
                .ToListAsync();

            ViewBag.Batches = batches;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns before returning the view again
                var batches = await _context.Batches
                    .Include(b => b.Sections)
                    .ToListAsync();

                ViewBag.Batches = batches;
                return View(student);
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            Console.WriteLine("Student Created: " + student.Name);

            return RedirectToAction(nameof(Index));
        }


        // Edit action to show the student edit form
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            // Passing Batches (with Sections) to the View
            ViewBag.Batches = _context.Batches.Include(b => b.Sections).ToList();
            return View(student);
        }

        // POST method to update the student data
        [HttpPost]
        public IActionResult Edit(Student updatedStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(updatedStudent);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // If model is invalid, return to view with data
            ViewBag.Batches = _context.Batches.Include(b => b.Sections).ToList();
            return View(updatedStudent);
        }

        // Details action to show the details of a student
        public IActionResult Details(int id)
        {
            var student = _context.Students
                .Include(s => s.Section)
                .ThenInclude(sec => sec.Batch)
                .FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // Delete action to remove a student from the database
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
