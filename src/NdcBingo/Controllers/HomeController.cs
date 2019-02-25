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
        private readonly IPlayerData _playerData;
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(IPlayerData playerData, ILogger<HomeController> logger)
        {
            _playerData = playerData;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var vm = new IndexViewModel();
            
            return View(vm);
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
