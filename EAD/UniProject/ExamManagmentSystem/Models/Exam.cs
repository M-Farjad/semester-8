namespace ExamManagmentSystem.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }

        public int BatchId { get; set; }
        public Batch Batch { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public ICollection<AttendanceSheet> AttendanceSheets { get; set; }
        public ICollection<SittingArrangement> SittingArrangements { get; set; }
    }

}
