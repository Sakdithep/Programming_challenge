using System.Reflection;
using Xunit;

namespace NaturalSort.Test
{
    public class NaturalSorterTests
    {
        [Theory]
        [InlineData(new[] { "TH19", "SG20", "TH2" }, new[] { "SG20", "TH2", "TH19" })]
        [InlineData(new[] { "TH10", "TH3Netflix", "TH1", "TH7" }, new[] { "TH1", "TH3Netflix", "TH7", "TH10" })]
        [InlineData(new[] { "file2", "file10", "file1" }, new[] { "file1", "file2", "file10" })]
        [InlineData(new[] { "img12.png", "img2.png", "img1.png" }, new[] { "img1.png", "img2.png", "img12.png" })]
        [InlineData(new[] { "a1", "A2", "a10" }, new[] { "a1", "A2", "a10" })]
        [InlineData(new[] { "100", "20", "3" }, new[] { "3", "20", "100" })]
        public void NaturalSort_ShouldSortCorrectly(string[] input, string[] expected)
        {
            var result = NaturalSorter.NaturalSort(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NaturalSort_ShouldHandleEmptyInput()
        {
            var result = NaturalSorter.NaturalSort(Array.Empty<string>());
            Assert.Empty(result);
        }

        [Fact]
        public void NaturalCompare_ShouldHandleNulls()
        {
            Assert.True(NaturalSorter.NaturalCompare(null, "a") < 0);
            Assert.True(NaturalSorter.NaturalCompare("a", null) > 0);
            Assert.Equal(0, NaturalSorter.NaturalCompare(null, null));
        }
    }
}
