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
        private const string PlayerSquaresKey = "player_squares";
        private const string PlayerClaimsKey = "player_claims";
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

        public bool TryGetPlayerSquares(out int[] ids)
        {
            if (!_contextAccessor.HttpContext.Request.Cookies.TryGetValue(PlayerSquaresKey, out var value))
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

        public void SetPlayerSquares(IEnumerable<Square> squares)
        {
            var value = string.Join(',', squares.Select(square => square.Id.ToString()));
            _contextAccessor.HttpContext.Response.Cookies.Append(PlayerSquaresKey, value, new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(7)
            });
        }
        
        public bool TryGetPlayerClaims(out int[] ids)
        {
            if (!_contextAccessor.HttpContext.Request.Cookies.TryGetValue(PlayerClaimsKey, out var value))
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

        public void SetPlayerClaims(int[] claims)
        {
            var value = string.Join(',', claims.Select(c => c.ToString()));
            
            _contextAccessor.HttpContext.Response.Cookies.Append(PlayerClaimsKey, value, new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(7),
                IsEssential = true
            });
        }
    }
}