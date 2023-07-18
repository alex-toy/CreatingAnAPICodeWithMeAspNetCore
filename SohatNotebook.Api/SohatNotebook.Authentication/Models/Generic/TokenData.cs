using System.ComponentModel.DataAnnotations;

namespace SohatNotebook.Authentication.Models.Generic
{
    public class TokenData
    {
        [Required]
        public string JwtToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
