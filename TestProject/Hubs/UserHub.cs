using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using TestProject.Models;
using Microsoft.AspNet.Identity;

namespace TestProject.Hubs
{
    public class UserHub : Hub
    {
        static List<ConnectedUser> Users = new List<ConnectedUser>();

        public void Connect()
        {
            var id = Context.ConnectionId;
            var userID = Context.User.Identity.GetUserId();
            if(!Users.Any(x => x.UserId == userID))
            {
                Users.Add(new ConnectedUser() { ConnectionId = id, Name = Context.User.Identity.Name, UserId = userID });             
            }
            else
            {
                var connectedUser = Users.FirstOrDefault(x => x.UserId == userID);
                connectedUser.ConnectionId = id;
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;               
            }

            return base.OnDisconnected(stopCalled);
        }

        public void Disconnect(string id)
        {
            var connectedUser = Users.FirstOrDefault(x => x.UserId == id);
            var context = GlobalHost.ConnectionManager.GetHubContext<UserHub>();
            if (connectedUser != null)
            {
                context.Clients.Client(connectedUser.ConnectionId).pageReload();
                Users.Remove(connectedUser);
            }
            
        }
    }
}