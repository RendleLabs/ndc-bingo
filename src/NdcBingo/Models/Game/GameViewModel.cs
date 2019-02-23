namespace NdcBingo.Models.Game
{
    public class GameViewModel
    {
        public string TalkName { get; set; }
        public SquareViewModel[] Squares { get; set; }
        public bool Winner { get; set; }
        public int[] Claims { get; set; }
    }
}