﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entites;
using Core.Entites.Concrete;

namespace Entities.DTOs
{
    public class UserForRegisterDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string  FirstName{ get; set; }
        public string LastName{ get; set; }
        public string Town { get; set; }
        public string Phone { get; set; }
        public string Rank { get; set; }

    }
}
