﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditRepository.Entities
{
    public class Users
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime Date { get; set; }

        public bool Deletet { get; set; }

    }
}
