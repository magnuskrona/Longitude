using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Longitude.Hubs
{
    public class MapHub : Hub
    {
        public void BroadcastPosition(string latitude, string longitude)
        {
            Clients.All.Send(latitude, longitude);
        }

    }
}