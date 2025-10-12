using System.Reflection;
using Xunit;

namespace NaturalSort.Test
{
    public class GetChunkTests
    {
        private static string InvokeGetChunk(string input, ref int index)
        {
            var method = typeof(NaturalSorter)
                .GetMethod("GetChunk", BindingFlags.NonPublic | BindingFlags.Static);

            return (string)method.Invoke(null, new object[] { input, index });
        }

        [Fact]
        public void GetChunk_ShouldReturnDigits()
        {
            int index = 0;
            var method = typeof(NaturalSorter)
                .GetMethod("GetChunk", BindingFlags.NonPublic | BindingFlags.Static);

            string result = (string)method.Invoke(null, new object[] { "123abc", index });
            Assert.Equal("123", result);
        }

        [Fact]
        public void GetChunk_ShouldReturnLetters()
        {
            int index = 0;
            var method = typeof(NaturalSorter)
                .GetMethod("GetChunk", BindingFlags.NonPublic | BindingFlags.Static);

            string result = (string)method.Invoke(null, new object[] { "abc123", index });
            Assert.Equal("abc", result);
        }

        [Fact]
        public void GetChunk_ShouldReturnEmpty_WhenIndexBeyondLength()
        {
            int index = 5;
            var method = typeof(NaturalSorter)
                .GetMethod("GetChunk", BindingFlags.NonPublic | BindingFlags.Static);

            string result = (string)method.Invoke(null, new object[] { "abc", index });
            Assert.Equal("", result);
        }

        [Fact]
        public void GetChunk_ShouldHandleMixedSequence()
        {
            string input = "a12b3";
            var method = typeof(NaturalSorter)
                .GetMethod("GetChunk", BindingFlags.NonPublic | BindingFlags.Static);

            int index = 0;
            string chunk1 = (string)method.Invoke(null, new object[] { input, index });
            Assert.Equal("a", chunk1);

            index += chunk1.Length;
            string chunk2 = (string)method.Invoke(null, new object[] { input, index });
            Assert.Equal("12", chunk2);

            index += chunk2.Length;
            string chunk3 = (string)method.Invoke(null, new object[] { input, index });
            Assert.Equal("b", chunk3);

            index += chunk3.Length;
            string chunk4 = (string)method.Invoke(null, new object[] { input, index });
            Assert.Equal("3", chunk4);
        }

        [Fact]
        public void GetChunk_ShouldHandleAllDigits()
        {
            int index = 0;
            var method = typeof(NaturalSorter)
                .GetMethod("GetChunk", BindingFlags.NonPublic | BindingFlags.Static);

            string result = (string)method.Invoke(null, new object[] { "987654", index });
            Assert.Equal("987654", result);
        }
    }
}
