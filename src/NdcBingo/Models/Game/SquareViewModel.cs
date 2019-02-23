namespace NdcBingo.Models.Game
{
    public class SquareViewModel
    {
        public SquareViewModel(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public bool Claimed { get; set; }
    }
}