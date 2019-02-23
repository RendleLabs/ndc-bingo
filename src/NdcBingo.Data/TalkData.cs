using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NdcBingo.Data
{
    public class TalkData : ITalkData
    {
        private readonly BingoContext _context;

        public TalkData(BingoContext context)
        {
            _context = context;
        }

        public async Task<Talk[]> GetCurrentTalks(DateTimeOffset now)
        {
            var talks = await _context.Talks.Where(t => t.StartTime <= now && t.EndTime >= now).ToArrayAsync();
            return talks;
        }

        public async Task<Talk> GetNextTalk(DateTimeOffset now)
        {
            return await _context.Talks.Where(t => t.StartTime > now).OrderBy(t => t.StartTime).FirstOrDefaultAsync();
        }

        public async Task<Talk> Get(int id)
        {
            var talk = await _context.Talks.FindAsync(id);
            return talk;
        }
    }
}