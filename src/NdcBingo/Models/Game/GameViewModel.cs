namespace NdcBingo.Models.Game
{
    public class GameViewModel
    {
        public int ColumnCount { get; set; }
        public SquareViewModel[] Squares { get; set; }
        public bool Winner { get; set; }
        public int[] Claims { get; set; }
    }
}