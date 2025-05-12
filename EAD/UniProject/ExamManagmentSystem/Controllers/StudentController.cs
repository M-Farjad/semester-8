using ExamManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagmentSystem.Controllers
{
    public class StudentController : Controller
    {
        // Simulated In-Memory list for frontend logic only
        private static List<Student> _students = new List<Student>
        {
            new Student { Id = 1, Name = "Ali", RollNumber = "2021-CS-A-01", Batch = "2021", Section = "CS(A)", Email = "ali@example.com", Phone = "1234567890" },
            new Student { Id = 2, Name = "Sara", RollNumber = "2022-CS-B-02", Batch = "2022", Section = "CS(B)", Email = "sara@example.com", Phone = "0987654321" }
        };

        public IActionResult Index(string batch, string section, string roll)
        {
            var filtered = _students.AsQueryable();
            if (!string.IsNullOrEmpty(batch))
                filtered = filtered.Where(s => s.Batch == batch);
            if (!string.IsNullOrEmpty(section))
                filtered = filtered.Where(s => s.Section == section);
            if (!string.IsNullOrEmpty(roll))
                filtered = filtered.Where(s => s.RollNumber.Contains(roll));

            ViewBag.Batch = batch;
            ViewBag.Section = section;
            ViewBag.Roll = roll;
            return View(filtered.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            student.Id = _students.Count + 1;
            _students.Add(student);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student updatedStudent)
        {
            var student = _students.FirstOrDefault(s => s.Id == updatedStudent.Id);
            if (student != null)
            {
                student.Name = updatedStudent.Name;
                student.Batch = updatedStudent.Batch;
                student.Section = updatedStudent.Section;
                student.RollNumber = updatedStudent.RollNumber;
                student.Email = updatedStudent.Email;
                student.Phone = updatedStudent.Phone;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            return View(student);
        }

        public IActionResult Delete(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            _students.Remove(student);
            return RedirectToAction("Index");
        }
    }

}
