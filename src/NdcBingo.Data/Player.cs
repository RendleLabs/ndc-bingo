using System.Linq;

namespace NdcBingo.Data
{
    public class Player
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Code => Base36.Encode(Id);
    }
}