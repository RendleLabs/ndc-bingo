using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace NdcBingo.Data
{
    public class SquareData : ISquareData
    {
        private readonly BingoContext _context;

        public SquareData(BingoContext context)
        {
            _context = context;
        }

        public async Task<Square[]> GetRandomSquaresAsync(int limit)
        {
            var cn = _context.Database.GetDbConnection();
            var squares = await cn.QueryAsync<Square>("select id, text, type, description from squares order by random() limit @limit", new {limit});
            return squares.ToArray();
        }

        public Task<Square[]> GetSquaresAsync(int[] ids) => _context.Squares.Where(s => ids.Contains(s.Id)).ToArrayAsync();
    }
}