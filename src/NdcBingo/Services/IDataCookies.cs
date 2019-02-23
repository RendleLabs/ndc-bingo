using System.Collections.Generic;
using NdcBingo.Data;

namespace NdcBingo.Services
{
    public interface IDataCookies
    {
        bool TryGetPlayerCode(out string code);
        void SetPlayerCode(string code);
        bool TryGetPlayerSquares(int talkId, out int[] ids);
        void SetPlayerSquares(int talkId, IEnumerable<Square> squares);
        bool TryGetPlayerClaims(int talkId, out int[] ids);
        void SetPlayerClaims(int talkId, int[] claims);
    }
}