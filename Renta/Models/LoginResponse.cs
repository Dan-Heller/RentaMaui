﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Models
{
    public class LoginResponse
    {
        public UserLookedUp user { get; set; }
        public string Token { get; set; }
    }
}
