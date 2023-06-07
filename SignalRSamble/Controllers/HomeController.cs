using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using SignalRSamble.cacheServise;
using SignalRSamble.Hubs;
using SignalRSamble.Models;
using System.Diagnostics;

namespace SignalRSamble.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHub;
        private readonly IDistributedCache _cache;

        public HomeController(ILogger<HomeController> logger,
            IHubContext<DeathlyHallowsHub> deathlyHub,
            IDistributedCache cache)
        {
            _logger = logger;
            _deathlyHub = deathlyHub;
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (SD.DeathlyHallowRace.ContainsKey(type))
            {
                int value = await _cache.GetRecordAsync<int>(type);
                await _cache.SetRecordAsync(type, ++value);
            }

            await _deathlyHub.Clients.All.SendAsync("updateDeathlyHallowsCount",
                await _cache.GetRecordAsync<int>("wand"),
                await _cache.GetRecordAsync<int>("cloak"),
                await _cache.GetRecordAsync<int>("stone"));

            return Accepted();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}