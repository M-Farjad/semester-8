using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEClassWeb.Models;

public partial class Uetuser
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Username is required.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }
}
