using System.ComponentModel.DataAnnotations;

namespace MathAPIClient.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
