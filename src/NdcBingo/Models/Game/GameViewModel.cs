namespace NdcBingo.Models.Game
{
    public class GameViewModel
    {
        public string PlayerName { get; set; }
        public int ColumnCount { get; set; }
        public SquareViewModel[] Squares { get; set; }
        public int WinningLines { get; set; }
        public int[] Claims { get; set; }
    }
}