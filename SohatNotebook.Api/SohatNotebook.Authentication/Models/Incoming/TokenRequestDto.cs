using System.ComponentModel.DataAnnotations;

namespace SohatNotebook.Authentication.Models.Incoming
{
    public class TokenRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
