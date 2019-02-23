using System;
using System.Buffers;
using System.Net;

namespace NdcBingo.Data
{
    public static class Base36
    {
        private static readonly char[] CharacterSet = "abcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();

        public static string Encode(long number)
        {
            if (number < 0) throw new ArgumentOutOfRangeException();
            
            var chars = ArrayPool<char>.Shared.Rent(16);
            try
            {
                int index = 0;
                while (number > 0)
                {
                    int digit = (int) (number % 36);
                    chars[index++] = CharacterSet[digit];
                    number = number / 36;
                }

                return new string(chars, 0, index);
            }
            finally
            {
                ArrayPool<char>.Shared.Return(chars);
            }
        }

        public static long Decode(string code)
        {
            long number = 0;
            long multiplier = 1;
            for (int i = 0, l = code.Length; i < l; i++)
            {
                int index = Array.IndexOf(CharacterSet, code[i]);
                if (index >= 0)
                {
                    number += index * multiplier;
                    multiplier *= 36;
                }
            }

            return number;
        }
    }
}