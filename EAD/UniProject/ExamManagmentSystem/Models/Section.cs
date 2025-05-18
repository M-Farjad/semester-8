namespace ExamManagmentSystem.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BatchId { get; set; }
        public Batch Batch { get; set; }

        // Existing navigation property
        public ICollection<Student> Students { get; set; }

        // Add this navigation property for Exams
        public ICollection<Exam> Exams { get; set; }
    }


}
