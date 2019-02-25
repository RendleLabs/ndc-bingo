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
        private readonly IPlayerData _playerData;
        private readonly ISquareData _squareData;
        private readonly IDataCookies _dataCookies;

        public GameController(ISquareData squareData, IDataCookies dataCookies, IPlayerData playerData)
        {
            _squareData = squareData;
            _dataCookies = dataCookies;
            _playerData = playerData;
        }

        [HttpGet]
        public async Task<IActionResult> Play()
        {
            if (!_dataCookies.TryGetPlayerCode(out var code))
            {
                return RedirectToAction("New", "Players");
            }
            
            var vm = await CreateGameViewModel();
            
            return View(vm);
        }

        private async Task<GameViewModel> CreateGameViewModel()
        {
            GameViewModel vm;
            if (_dataCookies.TryGetPlayerSquares(out var squareIds))
            {
                vm = await CreateGameInProgressViewModel(squareIds);
            }
            else
            {
                vm = await CreateNewGameViewModel();
            }

            return vm;
        }

        [HttpGet("claim/{squareId}")]
        public IActionResult Claim(int talkId, int squareId)
        {
            if (_dataCookies.TryGetPlayerSquares(out var squareIds))
            {
                int index = Array.IndexOf(squareIds, squareId);
                if (index > -1)
                {
                    if (!_dataCookies.TryGetPlayerClaims(out var claims))
                    {
                        claims = new int[Constants.SquareCount];
                    }

                    claims[index] = 1;
                    _dataCookies.SetPlayerClaims(claims);
                }
            }

            return RedirectToAction("Play", new {talkId});
        }

        private async Task<GameViewModel> CreateNewGameViewModel()
        {
            GameViewModel vm;
            var squares = await _squareData.GetRandomSquaresAsync(Constants.SquareCount);
            vm = new GameViewModel
            {
                Squares = squares.Select(s => new SquareViewModel(s.Id, s.Text)).ToArray(),
                Claims = new int[Constants.SquareCount]
            };
            _dataCookies.SetPlayerSquares(squares);
            _dataCookies.SetPlayerClaims(new int[Constants.SquareCount]);
            return vm;
        }

        private async Task<GameViewModel> CreateGameInProgressViewModel(int[] squareIds)
        {
            var squares = await _squareData.GetSquaresAsync(squareIds);
            var vm = new GameViewModel
            {
                Squares = squares.Select(s => new SquareViewModel(s.Id, s.Text)).ToArray()
            };
            if (_dataCookies.TryGetPlayerClaims(out var claims))
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