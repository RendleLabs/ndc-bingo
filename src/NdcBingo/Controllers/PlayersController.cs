using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Counter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NdcBingo.Data;
using NdcBingo.Models.Players;
using NdcBingo.Services;

namespace NdcBingo.Controllers
{
    [Route("players")]
    public class PlayersController : Controller
    {
        private readonly IPlayerData _playerData;
        private readonly IDataCookies _dataCookies;
        private readonly ILogger<PlayersController> _logger;
        private readonly IMetrics _metrics;

        public PlayersController(ILogger<PlayersController> logger, IDataCookies dataCookies, IPlayerData playerData, IMetrics metrics)
        {
            _logger = logger;
            _dataCookies = dataCookies;
            _playerData = playerData;
            _metrics = metrics;
        }

        [HttpGet("new")]
        public IActionResult New([FromQuery] string message, [FromQuery] string returnUrl)
        {
            var model = new NewViewModel
            {
                Message = message,
                Player = new NewPlayerViewModel(),
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromForm] NewPlayerViewModel model, [FromQuery]string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(model.Code))
            {
                var player = await _playerData.Get(model.Code);
                if (player == null)
                {
                    return RedirectToAction("New", new {message = "No matching code found."});
                }

                _dataCookies.SetPlayerCode(player.Code);
            }
            else if (!string.IsNullOrWhiteSpace(model.Name))
            {
                var (created, player) = await _playerData.TryCreate(model.Name);
                if (!created)
                {
                    return RedirectToAction("New", new {message = "Sorry, that name is taken."});
                }
                
                _dataCookies.SetPlayerCode(player.Code);
            }
            else
            {
                return RedirectToAction("New", new {message = "Enter a Name or Code."});
            }

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Play", "Game");
        }
    }

}