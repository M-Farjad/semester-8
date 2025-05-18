namespace ExamManagmentSystem.Models
{
    public class SittingArrangement
    {
        public int Id { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
    }

}
