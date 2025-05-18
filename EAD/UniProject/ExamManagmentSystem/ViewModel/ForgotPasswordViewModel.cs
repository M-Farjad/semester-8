using System.ComponentModel.DataAnnotations;

namespace ExamManagmentSystem.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
