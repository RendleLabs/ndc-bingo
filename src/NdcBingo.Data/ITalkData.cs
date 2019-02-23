using System;
using System.Threading.Tasks;

namespace NdcBingo.Data
{
    public interface ITalkData
    {
        Task<Talk[]> GetCurrentTalks(DateTimeOffset now);
        Task<Talk> GetNextTalk(DateTimeOffset now);
        Task<Talk> Get(int id);
    }
}