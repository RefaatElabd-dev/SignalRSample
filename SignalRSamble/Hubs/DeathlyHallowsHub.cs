using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using SignalRSamble.cacheServise;
using System.Runtime.InteropServices;

namespace SignalRSamble.Hubs
{
    public class DeathlyHallowsHub: Hub
    {
        private readonly IDistributedCache _cache;

        public DeathlyHallowsHub(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<Dictionary<string, int>> getRaceHallows()
        {
            var output = new Dictionary<string, int>();
            output.Add(SD.Wand, await _cache.GetRecordAsync<int>("Wand"));
            output.Add(SD.Cloak, await _cache.GetRecordAsync<int>("Cloak"));
            output.Add(SD.Stone, await _cache.GetRecordAsync<int>("Stone"));
            return output;
        }

    }
}
