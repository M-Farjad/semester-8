using System.ComponentModel.DataAnnotations;

public class Session
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}