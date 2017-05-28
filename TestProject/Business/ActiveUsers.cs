using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestProject.Models;

namespace TestProject.Business
{
    public static class ActiveUser
    {
       public static List<UserLogin> ActiveUsers { get; set; }   
       public static List<UserSession> UserSessions { get; set; }   
    }
}