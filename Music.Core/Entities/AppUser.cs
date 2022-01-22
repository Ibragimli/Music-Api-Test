﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
        public bool IsAdmin { get; set; }

    }
}
