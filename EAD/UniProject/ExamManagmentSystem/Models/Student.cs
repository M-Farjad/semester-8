namespace ExamManagmentSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string RollNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

    }


}
