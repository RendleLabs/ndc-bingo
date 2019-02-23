using System;
using Xunit;

namespace NdcBingo.Data.Tests
{
    public class Base36Tests
    {
        [Theory]
        [InlineData(123456L)]
        [InlineData(long.MaxValue)]
        public void EncodesAndDecodes(long expected)
        {
            var code = Base36.Encode(expected);
            var actual = Base36.Decode(code);
            Assert.Equal(expected, actual);
        }
    }
}
