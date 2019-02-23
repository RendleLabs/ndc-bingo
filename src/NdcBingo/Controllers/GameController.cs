using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NdcBingo.Data;
using NdcBingo.Game;
using NdcBingo.Models.Game;
using NdcBingo.Services;

namespace NdcBingo.Controllers
{
    [Route("game")]
    public class GameController : Controller
    {
        private readonly ITalkData _talkData;
        private readonly ISquareData _squareData;
        private readonly IDataCookies _dataCookies;

        public GameController(ITalkData talkData, ISquareData squareData, IDataCookies dataCookies)
        {
            _talkData = talkData;
            _squareData = squareData;
            _dataCookies = dataCookies;
        }

        [HttpGet("{talkId}")]
        public async Task<IActionResult> Get(int talkId)
        {
            var talk = await _talkData.Get(talkId);
            if (talk == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var vm = await CreateGameViewModel(talkId, talk);
            
            return View(vm);
        }

        private async Task<GameViewModel> CreateGameViewModel(int talkId, Talk talk)
        {
            GameViewModel vm;
            if (_dataCookies.TryGetPlayerSquares(talkId, out var squareIds))
            {
                vm = await CreateGameInProgressViewModel(talkId, squareIds, talk);
            }
            else
            {
                vm = await CreateNewGameViewModel(talkId, talk);
            }

            return vm;
        }

        [HttpGet("{talkId}/claim/{squareId}")]
        public IActionResult Claim(int talkId, int squareId)
        {
            if (_dataCookies.TryGetPlayerSquares(talkId, out var squareIds))
            {
                int index = Array.IndexOf(squareIds, squareId);
                if (index > -1)
                {
                    if (!_dataCookies.TryGetPlayerClaims(talkId, out var claims))
                    {
                        claims = new int[Constants.SquareCount];
                    }

                    claims[index] = 1;
                    _dataCookies.SetPlayerClaims(talkId, claims);
                }
            }

            return RedirectToAction("Get", new {talkId});
        }

        private async Task<GameViewModel> CreateNewGameViewModel(int talkId, Talk talk)
        {
            GameViewModel vm;
            var squares = await _squareData.GetRandomSquaresAsync(Constants.SquareCount);
            vm = new GameViewModel
            {
                TalkName = talk.Name,
                Squares = squares.Select(s => new SquareViewModel(s.Id, s.Text)).ToArray(),
                Claims = new int[Constants.SquareCount]
            };
            _dataCookies.SetPlayerSquares(talkId, squares);
            _dataCookies.SetPlayerClaims(talkId, new int[Constants.SquareCount]);
            return vm;
        }

        private async Task<GameViewModel> CreateGameInProgressViewModel(int id, int[] squareIds, Talk talk)
        {
            var squares = await _squareData.GetSquaresAsync(squareIds);
            var vm = new GameViewModel
            {
                TalkName = talk.Name,
                Squares = squares.Select(s => new SquareViewModel(s.Id, s.Text)).ToArray()
            };
            if (_dataCookies.TryGetPlayerClaims(id, out var claims))
            {
                for (int i = 0, l = Math.Min(vm.Squares.Length, claims.Length); i < l; i++)
                {
                    vm.Squares[i].Claimed = claims[i] > 0;
                }

                vm.Winner = WinCondition.Check(claims) != WinType.None;
            }

            return vm;
        }
    }
}