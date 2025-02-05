﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNet.Core.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public eRole Role { get; set; }

    }
    public enum eRole
    {
        user,employer,manager,subscription
    }
}
