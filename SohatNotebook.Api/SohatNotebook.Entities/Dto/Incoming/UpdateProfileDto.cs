﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SohatNotebook.Entities.Dto.Incoming
{
    public class UpdateProfileDto
    {
        public string Country { get; set; }
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }
        public string? Gender { get; set; }
    }
}
