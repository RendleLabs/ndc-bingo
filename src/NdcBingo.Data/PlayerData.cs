using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NdcBingo.Data
{
    public class PlayerData : IPlayerData
    {
        private readonly BingoContext _context;
        private readonly IPlayerIdGenerator _playerIdGenerator;

        public PlayerData(BingoContext context, IPlayerIdGenerator playerIdGenerator)
        {
            _context = context;
            _playerIdGenerator = playerIdGenerator;
        }

        public async Task<(bool, Player)> TryCreate(string name)
        {
            if (await _context.Players.AnyAsync(p => p.Name == name))
            {
                return (false, default);
            }

            long id;
            do
            {
                id = _playerIdGenerator.Get();
            } while (await _context.Players.AnyAsync(p => p.Id == id));

            var player = new Player
            {
                Id = id,
                Name = name
            };

            _context.Players.Add(player);
            try
            {
                await _context.SaveChangesAsync();
                return (true, player);
            }
            catch (Exception e)
            {
                return (false, default);
            }
        }

        public async Task<Player> Get(long id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            return player;
        }

        public async Task<Player> Get(string code)
        {
            var id = Base36.Decode(code);
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            return player;
        }
    }
}