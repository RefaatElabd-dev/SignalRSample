using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using SignalRSamble.cacheServise;
using System.Text;

namespace SignalRSamble.Hubs
{
    public class UserHub: Hub
    {
        private readonly IDistributedCache _cache;

        public UserHub(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> NewPageLoaded()
        {

            int _totalViews = await _cache.GetRecordAsync<int>("TotalViews");
            await _cache.SetRecordAsync("TotalViews", ++_totalViews);
            var x = _totalViews;
            //TotalViews++;
            await Clients.All.SendAsync("updateTotalViews", _totalViews);
            return $"number of views is: {_totalViews}";
        }

        public async override Task OnConnectedAsync()
        {
            int _totalUsers = await _cache.GetRecordAsync<int>("TotalUsers");
            await _cache.SetRecordAsync("TotalUsers", ++_totalUsers);
            //TotalUsers++;
            Clients.All.SendAsync("updateTotalUsers", _totalUsers).GetAwaiter().GetResult();
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            int _totalUsers = await _cache.GetRecordAsync<int>("TotalUsers");
            await _cache.SetRecordAsync("TotalUsers", --_totalUsers);
            //TotalUsers--;
            Clients.All.SendAsync("updateTotalUsers", _totalUsers).GetAwaiter().GetResult();
            await base.OnDisconnectedAsync(exception);
        }
    }
}
