using ExamManagmentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagmentSystem.Controllers
{
    [Authorize(Policy = "AdminAccess")]
    public class RoomController : BaseController
    {
        private static List<Room> _rooms = new List<Room>
        {
            new Room { Id = 1, RoomName = "G5", Rows = 5, CapacityPerRow = 10 },
            new Room { Id = 2, RoomName = "F16", Rows = 4, CapacityPerRow = 12 }
        };

        
        public IActionResult Index()
        {
            return View(_rooms);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            room.Id = _rooms.Count + 1;
            _rooms.Add(room);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(Room updatedRoom)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == updatedRoom.Id);
            if (room != null)
            {
                room.RoomName = updatedRoom.RoomName;
                room.Rows = updatedRoom.Rows;
                room.CapacityPerRow = updatedRoom.CapacityPerRow;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room != null) _rooms.Remove(room);
            return RedirectToAction("Index");
        }
    }


}
