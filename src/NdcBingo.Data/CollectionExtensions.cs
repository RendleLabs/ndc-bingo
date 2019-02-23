using System.Collections;

namespace NdcBingo.Data
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty(this ICollection collection) => collection == null || collection.Count == 0;
    }
}