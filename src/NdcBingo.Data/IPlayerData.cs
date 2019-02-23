using System.Threading.Tasks;

namespace NdcBingo.Data
{
    public interface IPlayerData
    {
        Task<(bool, Player)> TryCreate(string name);
        Task<Player> Get(long id);
        Task<Player> Get(string code);
    }
}