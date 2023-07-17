using System.ComponentModel.DataAnnotations;

namespace SohatNotebook.Entities.Dto.Incoming
{
    public class UserLoginRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
