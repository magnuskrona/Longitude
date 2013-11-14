using System.Collections.Generic;
using Microsoft.AspNet.SignalR;

namespace Longitude.Hubs
{
    public class MapHub : Hub
    {
        private static Dictionary<string, Client> _userMap = new Dictionary<string, Client>();  

        public void BroadcastPosition(string latitude, string longitude)
        {
            var client = new Client(Context.ConnectionId, Context.ConnectionId, "Description!", latitude, longitude);
            if (_userMap.ContainsKey(Context.ConnectionId))
                _userMap[Context.ConnectionId] = client;
            else
            {
                _userMap.Add(Context.ConnectionId, client);
            }

            Clients.All.Send(client, _userMap.Values.Count);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            Clients.Caller.Init(_userMap.Values);
            base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            if (_userMap.ContainsKey(Context.ConnectionId))
            {
                _userMap.Remove(Context.ConnectionId);
            }

            Clients.Others.Remove(Context.ConnectionId);
            return base.OnDisconnected();
        }
    }

    public class Client
    {
        public Client(string id, string name, string description, string latitude, string longitude)
        {
            Id = id;
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
}