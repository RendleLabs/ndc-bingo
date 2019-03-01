using System;

namespace NdcBingo.Game
{
    public class WinningLines
    {
        public int Horizontal { get; set; }
        public int Vertical { get; set; }
        public int Diagonal { get; set; }
    }
    public static class WinCondition
    {
        public static WinningLines Check(int[] claims)
        {
            double columnCountD = Math.Sqrt((double) claims.Length);
            if (Math.Abs(columnCountD % 1d) > double.Epsilon)
            {
                throw new ArgumentException("Array is not a square.");
            }

            int columnCount = (int) columnCountD;

            return new WinningLines
            {
                Horizontal = CheckHorizontalLines(claims, columnCount),
                Vertical = CheckVerticalLines(claims, columnCount),
                Diagonal = CheckDiagonalFromTopLeft(claims, columnCount) +
                           CheckDiagonalFromTopRight(claims, columnCount)
            };
        }

        private static int CheckDiagonalFromTopRight(int[] claims, int columnCount)
        {
            int claimed = 0;

            for (int i = columnCount - 1, c = 0; c < columnCount; i += columnCount - 1, c++)
            {
                claimed += OneOrZero(claims[i]);
            }

            return claimed == columnCount ? 1 : 0;
        }

        private static int CheckDiagonalFromTopLeft(int[] claims, int columnCount)
        {
            int claimed = 0;

            for (int i = 0, l = claims.Length; i < l; i += columnCount + 1)
            {
                claimed += OneOrZero(claims[i]);
            }

            return claimed == columnCount ? 1 : 0;
        }

        private static int CheckVerticalLines(int[] claims, int columnCount)
        {
            int count = 0;
            for (int i = 0; i < columnCount; i++)
            {
                int claimed = 0;
                for (int j = 0, l = claims.Length; j < l; j += columnCount)
                {
                    claimed += OneOrZero(claims[i + j]);
                }

                if (claimed == columnCount)
                {
                    count++;
                }
            }

            return count;
        }

        private static int CheckHorizontalLines(int[] claims, int columnCount)
        {
            int count = 0;
            for (int i = 0, l = claims.Length; i < l; i += columnCount)
            {
                int claimed = 0;
                for (int j = 0; j < columnCount; j++)
                {
                    claimed += OneOrZero(claims[i + j]);
                }

                if (claimed == columnCount)
                {
                    count++;
                }
            }

            return count;
        }

        private static int OneOrZero(int n) => n == 0 ? 0 : 1;
    }
}
