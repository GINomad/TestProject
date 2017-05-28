using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Business;
using TestProject.Hubs;

namespace TestProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {              

        public ActionResult UserWelcome()
        {
            RegisterSession(Session.SessionID);  
            return View();
        }
        [Authorize(Roles = "MasterAdmin")]
        public ActionResult MasterWelcome()
        {
            RegisterSession(Session.SessionID);
            return View(ActiveUser.UserSessions.Where(x => x.UserId != User.Identity.GetUserId()));
        }
        public ActionResult Index()
        {
            return RedirectToAction("Login","Account");
        }       

        private void RegisterSession(string SessionID)
        {
            var session = ActiveUser.UserSessions.FirstOrDefault(u => u.UserId == User.Identity.GetUserId());
            if (session != null)
            {
                if (SessionID != session.SessionId)
                {
                    session.SessionId = SessionID;
                }
            }
            else
            {
                ActiveUser.UserSessions.Add(new Models.UserSession()
                {
                    SessionId = SessionID,
                    UserId = User.Identity.GetUserId(),
                    UserName = User.Identity.Name
                });
            }
        }       
    }
}