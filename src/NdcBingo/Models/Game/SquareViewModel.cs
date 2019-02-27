namespace NdcBingo.Models.Game
{
    public class SquareViewModel
    {
        public SquareViewModel(int id, string text, string type, string description)
        {
            Id = id;
            Text = text;
            Type = type;
            Description = description;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string ClaimLink { get; set; }
        public bool Claimed { get; set; }
    }
}