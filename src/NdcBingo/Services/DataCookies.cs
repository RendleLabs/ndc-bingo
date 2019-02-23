using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using NdcBingo.Data;

namespace NdcBingo.Services
{
    public class DataCookies : IDataCookies
    {
        private const string PlayerNameKey = "player_name";
        private readonly IHttpContextAccessor _contextAccessor;

        public DataCookies(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool TryGetPlayerCode(out string code)
        {
            return _contextAccessor.HttpContext.Request.Cookies.TryGetValue(PlayerNameKey, out code);
        }

        public void SetPlayerCode(string code)
        {
            _contextAccessor.HttpContext.Response.Cookies.Append(PlayerNameKey, code, new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(7)
            });
        }

        private static string PlayerSquaresKey(long talkId) => $"player_squares_{talkId}";

        public bool TryGetPlayerSquares(int talkId, out int[] ids)
        {
            if (!_contextAccessor.HttpContext.Request.Cookies.TryGetValue(PlayerSquaresKey(talkId), out var value))
            {
                ids = null;
                return false;
            }

            ids = value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.TryParse(s, out int i) ? i : 0)
                .Where(i => i > 0)
                .ToArray();
            return true;
        }

        public void SetPlayerSquares(int talkId, IEnumerable<Square> squares)
        {
            var value = string.Join(',', squares.Select(square => square.Id.ToString()));
            _contextAccessor.HttpContext.Response.Cookies.Append(PlayerSquaresKey(talkId), value, new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(7)
            });
        }
        
        private static string PlayerClaimsKey(long talkId) => $"player_claims_{talkId}";
        
        public bool TryGetPlayerClaims(int talkId, out int[] ids)
        {
            if (!_contextAccessor.HttpContext.Request.Cookies.TryGetValue(PlayerClaimsKey(talkId), out var value))
            {
                ids = null;
                return false;
            }

            ids = value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.TryParse(s, out int i) ? i : 0)
                .Where(i => i > 0)
                .ToArray();
            
            return true;
        }

        public void SetPlayerClaims(int talkId, int[] claims)
        {
            var value = string.Join(',', claims.Select(c => c.ToString()));
            
            _contextAccessor.HttpContext.Response.Cookies.Append(PlayerClaimsKey(talkId), value, new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(7),
                IsEssential = true
            });
        }
    }
}