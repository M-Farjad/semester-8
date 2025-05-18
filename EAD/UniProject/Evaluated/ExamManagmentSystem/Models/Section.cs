namespace ExamManagmentSystem.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BatchId { get; set; }

        public Batch Batch { get; set; }
    }
}
