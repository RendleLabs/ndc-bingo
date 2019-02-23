using System.Threading.Tasks;
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

        public PlayersController(ILogger<PlayersController> logger, IDataCookies dataCookies, IPlayerData playerData)
        {
            _logger = logger;
            _dataCookies = dataCookies;
            _playerData = playerData;
        }

        [HttpGet("new")]
        public IActionResult New([FromQuery] string message, [FromQuery] string returnUrl)
        {
            var model = new NewViewModel
            {
                Message = message,
                Player = new NewPlayerViewModel {ReturnUrl = returnUrl}
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] NewPlayerViewModel model)
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
                    return RedirectToAction("New", new {message = "That name is taken."});
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

            return RedirectToAction("Index", "Home");
        }
    }
}