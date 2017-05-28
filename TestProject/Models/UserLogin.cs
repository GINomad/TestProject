using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class UserLogin
    {
        public string UserEmail { get; set; }

        public int FailedLoginCount { get; set; }

    }
}