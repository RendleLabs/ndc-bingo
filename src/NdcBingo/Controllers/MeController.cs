using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NdcBingo.Data;
using NdcBingo.Models.Me;
using NdcBingo.Services;

namespace NdcBingo.Controllers
{
    [Route("me")]
    public class MeController : Controller
    {
        private readonly IDataCookies _dataCookies;
        private readonly IPlayerData _playerData;
        private readonly ILogger<MeController> _logger;

        public MeController(IDataCookies dataCookies, IPlayerData playerData, ILogger<MeController> logger)
        {
            _dataCookies = dataCookies;
            _playerData = playerData;
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            if (_dataCookies.TryGetPlayerCode(out var code))
            {
                var player = await _playerData.Get(code);
                if (player != null)
                {
                    var vm = new MeViewModel
                    {
                        Code = player.Code,
                        Name = player.Name
                    };
                    return View(vm);
                }
            }
            
            _logger.LogInformation("New player");

            return RedirectToAction("New", "Players", new { returnUrl = Url.Action("Index")});
        }
    }
}