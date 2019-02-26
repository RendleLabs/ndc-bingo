using System;
using System.Security.Cryptography;

namespace NdcBingo.Data
{
    public class PlayerIdGenerator : IPlayerIdGenerator
    {
        private readonly RandomNumberGenerator _generator = new RNGCryptoServiceProvider();
        private readonly object _sync = new object();

        public long Get()
        {
            var bytes = new byte[8];
            lock (_sync)
            {
                _generator.GetBytes(bytes);
            }

            return Math.Abs(BitConverter.ToInt64(bytes, 0));
        }
    }
}