using System.Threading.Tasks;

namespace NdcBingo.Data
{
    public interface ISquareData
    {
        Task<Square[]> GetRandomSquaresAsync(int limit);
        Task<Square[]> GetSquaresAsync(int[] ids);
    }
}