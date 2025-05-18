using System.ComponentModel.DataAnnotations;

public class Section
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}