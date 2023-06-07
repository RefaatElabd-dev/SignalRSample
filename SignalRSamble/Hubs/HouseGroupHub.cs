using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using SignalRSamble.cacheServise;
using System.Reflection.Metadata.Ecma335;

namespace SignalRSamble.Hubs
{
    public class HouseGroupHub: Hub
    {
        private readonly IDistributedCache _cache;

        public HouseGroupHub(IDistributedCache cache)
        {
            _cache = cache;
        }
        //public static List<string> GroupsJoined { get; set; } = new List<String>();
        public async Task JoinHouse(string houseName)
        {
            var _groupsJoined = await _cache.GetRecordAsync<List<String>>("GroupsJoined") ?? new List<String>();
            if (!_groupsJoined.Contains(Context.ConnectionId + ":" + houseName))
            {
                _groupsJoined.Add(Context.ConnectionId + ":" + houseName);
                await _cache.SetRecordAsync("GroupsJoined", _groupsJoined);
            }

            string houseList = "";
            foreach (var item in _groupsJoined)
            {
                houseList += item.Split(":")[1] + " ";
            }

            await Clients.Caller.SendAsync("subscriptioStatue", houseList, houseName, true);
            await Clients.Others.SendAsync("newSubscriperAction", $"new Subscriper joined to {houseName}");
            await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
        }

        public async Task RemoveHouse(string houseName)
        {
            var _groupsJoined = await _cache.GetRecordAsync<List<String>>("GroupsJoined") ?? new List<String>();
            if (_groupsJoined.Contains(Context.ConnectionId + ":" + houseName))
            {
                _groupsJoined.Remove(Context.ConnectionId + ":" + houseName);
                await _cache.SetRecordAsync("GroupsJoined", _groupsJoined);
            }

            string houseList = "";
            foreach (var item in _groupsJoined)
            {
                houseList += item.Split(":")[1] + " ";
            }

            await Clients.Caller.SendAsync("subscriptioStatue", houseList, houseName, false);
            await Clients.Others.SendAsync("newSubscriperAction", $"A Subscriper Left {houseName}");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
        }

        public async Task TriggerHouseNotifications(string houseName)
        {
            await Clients.Group(houseName).SendAsync("TriggerHouseNotifications", houseName);
        }
    }
}
