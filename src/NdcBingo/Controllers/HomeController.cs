using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NdcBingo.Data;
using NdcBingo.Models;

namespace NdcBingo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITalkData _talkData;
        private readonly IPlayerData _playerData;
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ITalkData talkData, IPlayerData playerData, ILogger<HomeController> logger)
        {
            _talkData = talkData;
            _playerData = playerData;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var talks = await _talkData.GetCurrentTalks(DateTimeOffset.UtcNow);
            var vm = new IndexViewModel(talks);
            
            if (!talks.IsNullOrEmpty())
            {
                return View(vm);
            }

            var nextTalk = await _talkData.GetNextTalk(DateTimeOffset.UtcNow);
            if (nextTalk != null)
            {
                vm.NextTalkTime = nextTalk.StartTime.ToOffset(TimeSpan.FromHours(nextTalk.TimeZone));
            }
            return View("Landing", vm);
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
