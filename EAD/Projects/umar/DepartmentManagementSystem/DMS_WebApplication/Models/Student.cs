using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Student
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string RegNo { get; set; }
    [ForeignKey("Section")]
    public int SectionId { get; set; }
    public Section Section { get; set; }
    [ForeignKey("Session")]
    public int SessionId { get; set; }
    public Session Session { get; set; }
}