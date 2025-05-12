namespace ExamManagmentSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public int Rows { get; set; }
        public int CapacityPerRow { get; set; }

        public int TotalCapacity => Rows * CapacityPerRow;
    }

}
