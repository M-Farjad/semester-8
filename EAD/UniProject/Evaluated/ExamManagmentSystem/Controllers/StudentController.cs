using ExamManagmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagmentSystem.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    public class StudentController : BaseController
    {
        // Simulated In-Memory list for frontend logic only
        private static List<Student> _students = new List<Student>
        {
            new Student { Id = 1,  Name = "Ali Khan",        RollNumber = "2021-CS-A-01", Batch = "2021", Section = "CS(A)", Email = "ali.khan@example.com",        Phone = "03001234561" },
            new Student { Id = 2,  Name = "Sara Iqbal",      RollNumber = "2021-CS-A-02", Batch = "2021", Section = "CS(A)", Email = "sara.iqbal@example.com",      Phone = "03011234562" },
            new Student { Id = 3,  Name = "Hassan Raza",     RollNumber = "2021-CS-B-03", Batch = "2021", Section = "CS(B)", Email = "hassan.raza@example.com",     Phone = "03021234563" },
            new Student { Id = 4,  Name = "Ayesha Malik",    RollNumber = "2021-CS-B-04", Batch = "2021", Section = "CS(B)", Email = "ayesha.malik@example.com",    Phone = "03031234564" },
            new Student { Id = 5,  Name = "Usman Tariq",     RollNumber = "2021-IT-A-05", Batch = "2021", Section = "IT(A)", Email = "usman.tariq@example.com",     Phone = "03041234565" },

            new Student { Id = 6,  Name = "Fatima Javed",    RollNumber = "2022-SE-A-01", Batch = "2022", Section = "SE(A)", Email = "fatima.javed@example.com",    Phone = "03101234566" },
            new Student { Id = 7,  Name = "Bilal Ahmad",     RollNumber = "2022-SE-A-02", Batch = "2022", Section = "SE(A)", Email = "bilal.ahmad@example.com",     Phone = "03111234567" },
            new Student { Id = 8,  Name = "Zainab Saeed",    RollNumber = "2022-SE-B-03", Batch = "2022", Section = "SE(B)", Email = "zainab.saeed@example.com",    Phone = "03121234568" },
            new Student { Id = 9,  Name = "Ahmed Bashir",    RollNumber = "2022-SE-B-04", Batch = "2022", Section = "SE(B)", Email = "ahmed.bashir@example.com",    Phone = "03131234569" },
            new Student { Id = 10, Name = "Maha Farooq",     RollNumber = "2022-SE-B-05", Batch = "2022", Section = "SE(B)", Email = "maha.farooq@example.com",     Phone = "03141234570" },

            new Student { Id = 11, Name = "Hamza Siddiqui",  RollNumber = "2023-CS-A-01", Batch = "2023", Section = "CS(A)", Email = "hamza.siddiqui@example.com",  Phone = "03201234571" },
            new Student { Id = 12, Name = "Rubab Ali",       RollNumber = "2023-CS-A-02", Batch = "2023", Section = "CS(A)", Email = "rubab.ali@example.com",       Phone = "03211234572" },
            new Student { Id = 13, Name = "Shahid Mehmood",  RollNumber = "2023-CS-B-03", Batch = "2023", Section = "CS(B)", Email = "shahid.mehmood@example.com",  Phone = "03221234573" },
            new Student { Id = 14, Name = "Nimra Yousaf",    RollNumber = "2023-CS-B-04", Batch = "2023", Section = "CS(B)", Email = "nimra.yousaf@example.com",    Phone = "03231234574" },
            new Student { Id = 15, Name = "Faisal Qureshi",  RollNumber = "2023-CS-B-05", Batch = "2023", Section = "CS(B)", Email = "faisal.qureshi@example.com",  Phone = "03241234575" },

            new Student { Id = 16, Name = "Hira Nawaz",      RollNumber = "2024-SE-A-01", Batch = "2022", Section = "SE(A)", Email = "hira.nawaz@example.com",      Phone = "03301234576" },
            new Student { Id = 17, Name = "Taha Sheikh",     RollNumber = "2024-SE-A-02", Batch = "2022", Section = "SE(A)", Email = "taha.sheikh@example.com",     Phone = "03311234577" },
            new Student { Id = 18, Name = "Khadija Hassan",  RollNumber = "2024-SE-A-03", Batch = "2022", Section = "SE(A)", Email = "khadija.hassan@example.com",  Phone = "03321234578" },
            new Student { Id = 19, Name = "Raheel Anwar",    RollNumber = "2024-SE-A-04", Batch = "2022", Section = "SE(B)", Email = "raheel.anwar@example.com",    Phone = "03331234579" },
            new Student { Id = 20, Name = "Aqsa Zafar",      RollNumber = "2024-SE-A-05", Batch = "2022", Section = "SE(B)", Email = "aqsa.zafar@example.com",      Phone = "03341234580" }
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
