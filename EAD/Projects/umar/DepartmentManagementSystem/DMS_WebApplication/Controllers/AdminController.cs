using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using ExcelDataReader;
using System.Data;

public class AdminController : Controller
{
    private readonly AppDbContext _db;
    public AdminController(AppDbContext db) { _db = db; }

    // List students
    public IActionResult Index()
    {
        ViewBag.Sections = _db.Sections.ToList();
        ViewBag.Sessions = _db.Sessions.ToList();
        return View(_db.Students.Include(s => s.Section).Include(s => s.Session).ToList());
    }

    // Add student
    [HttpPost]
    public IActionResult Add(string name, string regNo, int sectionId, int sessionId)
    {
        if (_db.Students.Any(s => s.RegNo == regNo))
        {
            TempData["Error"] = "A student with this Reg# already exists. Please enter a unique Reg#.";
            return RedirectToAction("Index");
        }
        _db.Students.Add(new Student { Name = name, RegNo = regNo, SectionId = sectionId, SessionId = sessionId });
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    // Delete student
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var s = _db.Students.Find(id);
        if (s != null)
        {
            _db.Students.Remove(s);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    // Show the edit form
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var student = _db.Students.Find(id);
        ViewBag.Sections = _db.Sections.ToList();
        ViewBag.Sessions = _db.Sessions.ToList();
        return View(student);
    }

    // Handle the edit form POST
    [HttpPost]
    public IActionResult Edit(int id, string name, string regNo, int sectionId, int sessionId)
    {
        // Check for duplicate RegNo (excluding the current student)
        var duplicate = _db.Students.FirstOrDefault(s => s.RegNo == regNo && s.Id != id);
        if (duplicate != null)
        {
            TempData["Error"] = "A student with this Reg# already exists. Please enter a unique Reg#.";
            return RedirectToAction("Edit", new { id });
        }

        var s = _db.Students.Find(id);
        if (s != null)
        {
            s.Name = name;
            s.RegNo = regNo;
            s.SectionId = sectionId;
            s.SessionId = sessionId;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "A student with this Reg# already exists (database constraint). Please enter a unique Reg#.";
                return RedirectToAction("Edit", new { id });
            }
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ImportExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            TempData["Error"] = "Please select a valid Excel file.";
            return RedirectToAction("Index");
        }

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using (var stream = file.OpenReadStream())
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            var result = reader.AsDataSet();
            var table = result.Tables[0];

            for (int i = 1; i < table.Rows.Count; i++) // Start from 1 to skip header
            {
                var name = table.Rows[i][0]?.ToString();
                var regNo = table.Rows[i][1]?.ToString();
                var sectionName = table.Rows[i][2]?.ToString();
                var sessionName = table.Rows[i][3]?.ToString();

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(regNo) ||
                    string.IsNullOrWhiteSpace(sectionName) || string.IsNullOrWhiteSpace(sessionName))
                    continue; // skip invalid rows

                // Check for duplicate RegNo
                if (_db.Students.Any(s => s.RegNo == regNo))
                    continue;

                // Find Section and Session by name
                var section = _db.Sections.FirstOrDefault(s => s.Name == sectionName);
                var session = _db.Sessions.FirstOrDefault(s => s.Name == sessionName);

                if (section == null || session == null)
                    continue; // skip if section/session not found

                _db.Students.Add(new Student
                {
                    Name = name,
                    RegNo = regNo,
                    SectionId = section.Id,
                    SessionId = session.Id
                });
            }
            _db.SaveChanges();
        }

        TempData["Success"] = "Students imported successfully!";
        return RedirectToAction("Index");
    }
}