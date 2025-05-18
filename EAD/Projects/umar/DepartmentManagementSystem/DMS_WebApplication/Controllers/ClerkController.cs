using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Rotativa.AspNetCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class ClerkController : Controller
{
    private readonly AppDbContext _db;
    public ClerkController(AppDbContext db) { _db = db; }

    public IActionResult Index()
    {
        var students = _db.Students
            .Include(s => s.Section)
            .Include(s => s.Session)
            .ToList();
        return View(students);
    }

    // Helper to group students into rooms by Reg# prefix (e.g., 2021-SE, 2022-CS, etc.)
    private List<RoomGroup> GetRoomGroups()
    {
        var students = _db.Students
            .Include(s => s.Section)
            .Include(s => s.Session)
            .OrderBy(s => s.Session.Name)
            .ThenBy(s => s.Section.Name)
            .ThenBy(s => s.RegNo)
            .ToList();

        var groups = new List<RoomGroup>();
        int globalRoomNumber = 1;

        // Group by Session.Name + Section.Name
        var classGroups = students
            .GroupBy(s => $"{s.Session.Name}-{s.Section.Name}");

        foreach (var classGroup in classGroups)
        {
            var classStudents = classGroup.ToList();
            int roomCount = (int)Math.Ceiling(classStudents.Count / 20.0);

            for (int i = 0; i < roomCount; i++)
            {
                var roomStudents = classStudents.Skip(i * 20).Take(20).ToList();
                groups.Add(new RoomGroup
                {
                    RoomName = $"Room{globalRoomNumber}", // Globally unique room number
                    Students = roomStudents
                });
                globalRoomNumber++;
            }
        }

        return groups;
    }

    public IActionResult GenerateSittingPlan()
    {
        var rooms = GetRoomGroups();
        return new ViewAsPdf("SittingPlanPdf", rooms);
    }

    public IActionResult GenerateAttendanceSheet()
    {
        var rooms = GetRoomGroups();
        return new ViewAsPdf("AttendanceSheetPdf", rooms);
    }
}