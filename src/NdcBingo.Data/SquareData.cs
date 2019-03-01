using System;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace NdcBingo.Data
{
    public class SquareData : ISquareData
    {
        private readonly BingoContext _context;
        private readonly IMetrics _metrics;

        public SquareData(BingoContext context, IMetrics metrics)
        {
            _context = context;
            _metrics = metrics;
        }

        public async Task<Square[]> GetRandomSquaresAsync(int limit)
        {
            using (_metrics.RandomSquaresQueryTimer())
            {
                var cn = _context.Database.GetDbConnection();
                var squares = await cn.QueryAsync<Square>(
                    "select id, text, type, description from squares order by random() limit @limit", new {limit});
                return squares.ToArray();
            }
        }

        public async Task<Square[]> GetSquaresAsync(int[] ids)
        {
            using (_metrics.SquaresQueryTimer())
            {
                var squares = new Square[ids.Length];
                foreach (var s in await _context.Squares.Where(s => ids.Contains(s.Id)).ToListAsync())
                {
                    int index = Array.IndexOf(ids, s.Id);
                    if (index > -1)
                    {
                        squares[index] = s;
                    }
                }

                return squares;
            }
        }
    }
}