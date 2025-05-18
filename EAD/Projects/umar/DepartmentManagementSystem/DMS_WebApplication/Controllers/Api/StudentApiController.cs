using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class StudentApiController : ControllerBase
{
    private readonly AppDbContext _db;
    public StudentApiController(AppDbContext db) { _db = db; }

    [HttpGet]
    public IActionResult Get()
    {
        var students = _db.Students
            .Include(s => s.Section)
            .Include(s => s.Session)
            .Select(s => new {
                id = s.Id,
                name = s.Name,
                regNo = s.RegNo,           
                section = s.Section.Name,
                session = s.Session.Name
            }).ToList();
        return Ok(students);
    }
}