using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Longitude.Hubs
{
    public class MapHub : Hub
    {
        private IDictionary<string, Client> _userMap = new Dictionary<string, Client>();  

        public void BroadcastPosition(string latitude, string longitude)
        {
            var id = Context.ConnectionId;
            var client = new Client(id, "Description!", latitude, longitude);
            if (_userMap.ContainsKey(id))
                _userMap[id] = client;
            else
            {
                _userMap.Add(id, client);
            }

            Clients.All.Send(client, _userMap.Values.Count);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            Clients.Caller.Init(_userMap.Values);

            return base.OnConnected();
        }
    }

    public class Client
    {
        public Client(string name, string description, string latitude, string longitude)
        {
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
}