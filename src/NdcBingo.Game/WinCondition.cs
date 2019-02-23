using System;

namespace NdcBingo.Game
{
    public static class WinCondition
    {
        public static WinType Check(int[] claims)
        {
            double columnCountD = Math.Sqrt((double) claims.Length);
            if (Math.Abs(columnCountD % 1d) > double.Epsilon)
            {
                throw new ArgumentException("Array is not a square.");
            }

            int columnCount = (int) columnCountD;

            if (TryHorizontalLine(claims, columnCount, out var horizontalLine)) return horizontalLine;

            if (TryVerticalLine(claims, columnCount, out var verticalLine)) return verticalLine;

            if (TryDiagonalFromTopLeft(claims, columnCount, out var diagonalLine)) return diagonalLine;

            if (TryDiagonalFromTopRight(claims, columnCount, out var winType)) return winType;

            return WinType.None;
        }

        private static bool TryDiagonalFromTopRight(int[] claims, int columnCount, out WinType winType)
        {
            int claimed = 0;

            for (int i = columnCount - 1, l = claims.Length; i < l; i += columnCount - 1)
            {
                claimed += OneOrZero(claims[i]);
            }

            if (claimed == columnCount)
            {
                winType = WinType.DiagonalLine;
                return true;
            }

            winType = default;
            return false;
        }

        private static bool TryDiagonalFromTopLeft(int[] claims, int columnCount, out WinType winType)
        {
            int claimed = 0;

            for (int i = 0, l = claims.Length; i < l; i += columnCount + 1)
            {
                claimed += OneOrZero(claims[i]);
            }

            if (claimed == columnCount)
            {
                winType = WinType.DiagonalLine;
                return true;
            }

            winType = default;
            return false;
        }

        private static bool TryVerticalLine(int[] claims, int columnCount, out WinType winType)
        {
            for (int i = 0; i < columnCount; i++)
            {
                int claimed = 0;
                for (int j = 0, l = claims.Length; j < l; j += columnCount)
                {
                    claimed += OneOrZero(claims[i + j]);
                }

                if (claimed == columnCount)
                {
                    winType = WinType.VerticalLine;
                    return true;
                }
            }

            winType = default;
            return false;
        }

        private static bool TryHorizontalLine(int[] claims, int columnCount, out WinType winType)
        {
            for (int i = 0, l = claims.Length; i < l; i += columnCount)
            {
                int claimed = 0;
                for (int j = 0; j < columnCount; j++)
                {
                    claimed += OneOrZero(claims[i + j]);
                }

                if (claimed == columnCount)
                {
                    winType = WinType.HorizontalLine;
                    return true;
                }
            }

            winType = default;
            return false;
        }

        private static int OneOrZero(int n) => n == 0 ? 0 : 1;
    }
}
