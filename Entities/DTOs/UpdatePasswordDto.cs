﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UpdatePasswordDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
    }
}
