using System.Collections.Generic;
using NdcBingo.Data;

namespace NdcBingo.Services
{
    public interface IDataCookies
    {
        bool TryGetPlayerCode(out string code);
        void SetPlayerCode(string code);
        bool TryGetPlayerSquares(out int[] ids);
        void SetPlayerSquares(IEnumerable<Square> squares);
        bool TryGetPlayerClaims(out int[] ids);
        void SetPlayerClaims(int[] claims);
    }
}