﻿using System.ComponentModel.DataAnnotations;

namespace SohatNotebook.Authentication.Models.Incoming
{
    public class TokenRequestDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
