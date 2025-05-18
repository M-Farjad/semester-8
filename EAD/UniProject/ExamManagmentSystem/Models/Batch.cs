namespace ExamManagmentSystem.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public int Year { get; set; }

        // Existing navigation property
        public ICollection<Section> Sections { get; set; }

        // Add this navigation property for Exams
        public ICollection<Exam> Exams { get; set; }
    }

}
