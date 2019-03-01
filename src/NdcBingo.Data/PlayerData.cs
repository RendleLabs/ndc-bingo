using System;
using System.Threading.Tasks;
using App.Metrics;
using Microsoft.EntityFrameworkCore;

namespace NdcBingo.Data
{
    public class PlayerData : IPlayerData
    {
        private readonly BingoContext _context;
        private readonly IPlayerIdGenerator _playerIdGenerator;
        private readonly IMetrics _metrics;

        public PlayerData(BingoContext context, IPlayerIdGenerator playerIdGenerator, IMetrics metrics)
        {
            _context = context;
            _playerIdGenerator = playerIdGenerator;
            _metrics = metrics;
        }

        public async Task<(bool, Player)> TryCreate(string name)
        {
            using (_metrics.PlayerQueryTimer(nameof(name)))
            {
                if (await _context.Players.AnyAsync(p => p.Name == name))
                {
                    return (false, default);
                }
            }

            var id = await GeneratePlayerIdAsync();

            var player = new Player
            {
                Id = id,
                Name = name
            };

            return await SavePlayerAsync(player);
        }

        private async Task<(bool, Player)> SavePlayerAsync(Player player)
        {
            _context.Players.Add(player);
            try
            {
                using (_metrics.PlayerCreateTimer())
                {
                    await _context.SaveChangesAsync();
                }

                int playerCount;
                using (_metrics.PlayerCountQueryTimer())
                {
                    playerCount = await _context.Players.CountAsync();
                }

                _metrics.SetPlayerCount(playerCount);

                return (true, player);
            }
            catch (Exception e)
            {
                return (false, default);
            }
        }

        private async Task<long> GeneratePlayerIdAsync()
        {
            long id;
            using (_metrics.PlayerIdDuplicateCheckTimer())
            {
                do
                {
                    id = _playerIdGenerator.Get();
                } while (await _context.Players.AnyAsync(p => p.Id == id));
            }

            return id;
        }

        public async Task<Player> Get(long id)
        {
            using (_metrics.PlayerQueryTimer(nameof(id)))
            {
                var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
                return player;
            }
        }

        public async Task<Player> Get(string code)
        {
            var id = Base36.Decode(code);
            using (_metrics.PlayerQueryTimer(nameof(code)))
            {
                var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
                return player;
            }
        }
    }
}