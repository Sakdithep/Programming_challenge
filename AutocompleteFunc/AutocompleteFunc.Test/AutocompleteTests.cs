using System;
using Xunit;

namespace AutocompleteFunc.Tests
{
    public class AutocompleteTests
    {
        #region Input Validation Tests
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void Autocomplete_SearchIsNullOrWhitespace_ReturnsEmpty(string search)
        {
            string[] items = { "Apple", "Banana", "Cat" };

            var result = AutocompleteHelper.Autocomplete(search, items, 5);

            Assert.Empty(result);
        }

        [Fact]
        public void Autocomplete_ItemsIsNull_ReturnsEmpty()
        {
            string[] items = null;

            var result = AutocompleteHelper.Autocomplete("test", items, 5);

            Assert.Empty(result);
        }

        [Fact]
        public void Autocomplete_ItemsIsEmpty_ReturnsEmpty()
        {
            string[] items = Array.Empty<string>();

            var result = AutocompleteHelper.Autocomplete("test", items, 5);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Autocomplete_MaxResultIsZeroOrNegative_ThrowsArgumentOutOfRangeException(int maxResult)
        {
            string[] items = { "Apple", "Banana" };

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                AutocompleteHelper.Autocomplete("a", items, maxResult);
            });

            Assert.Contains("maxResult", exception.Message);
        }

        #endregion

        #region Priority Tests
        [Fact]
        public void Autocomplete_StartsWithPriority_ComesFirst()
        {
            string[] items = { "Mother", "Theater", "Think" };

            var result = AutocompleteHelper.Autocomplete("th", items, 10);

            Assert.Equal(3, result.Length);
            Assert.Equal("Theater", result[0]);
            Assert.Equal("Think", result[1]);
            Assert.Equal("Mother", result[2]);
        }

        [Fact]
        public void Autocomplete_ContainsPriority_ComesAfterStartsWith()
        {
            string[] items = { "Rhythm", "Father", "Theater" };

            var result = AutocompleteHelper.Autocomplete("th", items, 10);

            Assert.Equal(3, result.Length);
            Assert.Equal("Theater", result[0]);
            Assert.Equal("Father", result[1]);
            Assert.Equal("Rhythm", result[2]);
        }

        [Fact]
        public void Autocomplete_EndsWithPriority_ComesLast()
        {
            string[] items = { "growth", "throw", "thrive" };

            var result = AutocompleteHelper.Autocomplete("th", items, 10);

            Assert.Equal(3, result.Length);
            Assert.Equal("thrive", result[0]); 
            Assert.Equal("throw", result[1]);  
            Assert.Equal("growth", result[2]); 
        }

        [Fact]
        public void Autocomplete_SamePriority_SortsAlphabetically()
        {
            string[] items = { "Think", "Theater", "Throw", "Thread" };

            var result = AutocompleteHelper.Autocomplete("th", items, 10);

            Assert.Equal(4, result.Length);
            Assert.Equal("Theater", result[0]);
            Assert.Equal("Think", result[1]);
            Assert.Equal("Thread", result[2]);
            Assert.Equal("Throw", result[3]);
        }

        #endregion

        #region Case Sensitivity Tests
        [Theory]
        [InlineData("do")]
        [InlineData("DO")]
        [InlineData("Do")]
        [InlineData("dO")]
        public void Autocomplete_CaseInsensitiveSearch_FindsMatches(string search)
        {
            string[] items = { "Dog", "DOOR", "dome", "Apple" };

            var result = AutocompleteHelper.Autocomplete(search, items, 10);

            Assert.Equal(3, result.Length);
            Assert.Contains("Dog", result);
            Assert.Contains("DOOR", result);
            Assert.Contains("dome", result);
        }

        [Fact]
        public void Autocomplete_PreservesOriginalCase()
        {
            string[] items = { "DOG", "Dog", "dog" };

            var result = AutocompleteHelper.Autocomplete("do", items, 10);

            Assert.Equal(3, result.Length);
            Assert.Contains("DOG", result);
            Assert.Contains("Dog", result);
            Assert.Contains("dog", result);
        }

        #endregion

        #region MaxResult Limit Tests
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Autocomplete_RespectsMaxResultLimit(int maxResult)
        {
            string[] items = { "Think", "Mother", "Worthy", "Something", "Theater" };

            var result = AutocompleteHelper.Autocomplete("th", items, maxResult);

            Assert.Equal(maxResult, result.Length);
        }

        [Fact]
        public void Autocomplete_FewerMatchesThanMaxResult_ReturnsAllMatches()
        {
            string[] items = { "Think", "Mother" };

            var result = AutocompleteHelper.Autocomplete("th", items, 10);

            Assert.Equal(2, result.Length);
            Assert.All(result, item => Assert.NotNull(item));
        }

        [Fact]
        public void Autocomplete_MaxResultOne_ReturnsTopPriority()
        {
            string[] items = { "Mother", "Think", "Theater" };

            var result = AutocompleteHelper.Autocomplete("th", items, 1);

            Assert.Single(result);
            Assert.Equal("Theater", result[0]);
        }

        #endregion

        #region No Match Tests
        [Fact]
        public void Autocomplete_NoMatch_ReturnsEmpty()
        {
            string[] items = { "Apple", "Banana", "Cat" };

            var result = AutocompleteHelper.Autocomplete("zzz", items, 5);

            Assert.Empty(result);
        }

        [Fact]
        public void Autocomplete_SearchLongerThanAllItems_ReturnsEmpty()
        {
            string[] items = { "Dog", "Cat" };

            var result = AutocompleteHelper.Autocomplete("elephant", items, 5);

            Assert.Empty(result);
        }

        #endregion

        #region Whitespace Handling Tests
        [Fact]
        public void Autocomplete_ItemsContainNullOrWhitespace_IgnoresThem()
        {
            string[] items = { "  ", "  Think ", null, " Mother ", "", "Worthy" };

            var result = AutocompleteHelper.Autocomplete("th", items, 10);

            Assert.Equal(3, result.Length);
            Assert.Contains("Think", result);
            Assert.Contains("Mother", result);
            Assert.Contains("Worthy", result);
            Assert.All(result, item =>
            {
                Assert.NotNull(item);
                Assert.NotEqual("", item);
                Assert.NotEqual("  ", item);
            });
        }

        [Fact]
        public void Autocomplete_TrimsWhitespaceFromItems()
        {
            string[] items = { "  Think  ", "  Mother  ", "  Theater  " };

            var result = AutocompleteHelper.Autocomplete("th", items, 10);

            Assert.All(result, item =>
            {
                Assert.Equal(item.Trim(), item);
            });
        }

        [Fact]
        public void Autocomplete_TrimsSearchString()
        {
            string[] items = { "Think", "Mother", "Theater" };

            var result1 = AutocompleteHelper.Autocomplete("  th  ", items, 10);
            var result2 = AutocompleteHelper.Autocomplete("th", items, 10);
            Assert.Equal(result2, result1);
        }

        #endregion

        #region Exact Match Tests
        [Fact]
        public void Autocomplete_ExactMatch_TreatedAsStartsWith()
        {
            string[] items = { "the", "there", "their" };

            var result = AutocompleteHelper.Autocomplete("the", items, 10);

            Assert.Equal(3, result.Length);
            Assert.Equal("the", result[0]);
            Assert.Equal("their", result[1]);
            Assert.Equal("there", result[2]);
        }

        [Fact]
        public void Autocomplete_MultipleExactMatchesDifferentCase_ReturnsAll()
        {
            string[] items = { "DOG", "dog", "Dog", "dOg" };

            var result = AutocompleteHelper.Autocomplete("dog", items, 10);

            Assert.Equal(4, result.Length);
        }

        #endregion

        #region Edge Cases Tests
        [Fact]
        public void Autocomplete_SingleCharacterSearch_Works()
        {
            string[] items = { "Apple", "Banana", "Apricot", "Cat" };

            var result = AutocompleteHelper.Autocomplete("a", items, 10);

            Assert.Equal(4, result.Length);
            Assert.Contains("Apple", result);
            Assert.Contains("Apricot", result);
            Assert.Contains("Banana", result);
            Assert.Contains("Cat", result);
        }

        [Fact]
        public void Autocomplete_SingleCharacterItems_Works()
        {
            string[] items = { "a", "b", "c", "ab" };

            var result = AutocompleteHelper.Autocomplete("a", items, 10);

            Assert.Equal(2, result.Length);
            Assert.Contains("a", result);
            Assert.Contains("ab", result);
        }

        [Fact]
        public void Autocomplete_LargeDataset_WorksEfficiently()
        {
            var items = new string[1000];
            for (int i = 0; i < 1000; i++)
            {
                items[i] = $"Item{i}";
            }
            items[500] = "TestItem";
            items[501] = "ItemTest";

            var result = AutocompleteHelper.Autocomplete("test", items, 10);

            Assert.Equal(2, result.Length);
            Assert.Contains("TestItem", result);
            Assert.Contains("ItemTest", result);
            Assert.Equal("TestItem", result[0]);
        }

        [Fact]
        public void Autocomplete_SpecialCharacters_Works()
        {
            string[] items = { "C++", "C#", "C", "JavaScript" };

            var result = AutocompleteHelper.Autocomplete("c", items, 10);

            Assert.Equal(4, result.Length);
            Assert.Contains("C", result);
            Assert.Contains("C#", result);
            Assert.Contains("C++", result);
            Assert.Contains("JavaScript", result);
        }

        [Fact]
        public void Autocomplete_DuplicateItems_ReturnsAllDuplicates()
        {
            string[] items = { "Think", "Think", "Think", "Mother" };

            var result = AutocompleteHelper.Autocomplete("th", items, 10);

            Assert.Equal(4, result.Length);
            Assert.Equal(3, result.Count(x => x == "Think"));
        }

        #endregion

        #region Integration Tests

        [Fact]
        public void Autocomplete_RealWorldScenario_SortsCorrectly()
        {
            string[] items =
            {
                "Python",
                "Theater",
                "Mathematics",
                "Growth", 
                "Think",   
                "Algorithm",
                "The",
                "Health"
            };

            var result = AutocompleteHelper.Autocomplete("th", items, 10);

            Assert.Equal(8, result.Length);

            Assert.Equal("The", result[0]);
            Assert.Equal("Theater", result[1]);
            Assert.Equal("Think", result[2]);
            Assert.Equal("Algorithm", result[3]);
            Assert.Equal("Mathematics", result[4]);
            Assert.Equal("Python", result[5]);
            Assert.Equal("Growth", result[6]);
            Assert.Equal("Health", result[7]);
        }

        [Fact]
        public void Autocomplete_ProductSearch_WorksAsExpected()
        {
            string[] products =
            {
                "iPhone 15 Pro",
                "iPad Air",
                "MacBook Pro",
                "AirPods Pro",
                "Apple Watch",
                "Mac Studio",
                "HomePod"
            };

            var result = AutocompleteHelper.Autocomplete("pro", products, 5);

            Assert.Equal(3, result.Length);
            Assert.Contains("AirPods Pro", result);    // EndsWith
            Assert.Contains("iPhone 15 Pro", result);  // EndsWith
            Assert.Contains("MacBook Pro", result);    // EndsWith
        }

        [Fact]
        public void Autocomplete_NameSearch_WorksAsExpected()
        {
            string[] names =
            {
                "John Smith",
                "Jane Doe",
                "John Doe",
                "Jonathan Williams",
                "Smith John"
            };

            var result = AutocompleteHelper.Autocomplete("john", names, 10);

            Assert.Equal(3, result.Length);

            Assert.Equal("John Doe", result[0]);
            Assert.Equal("John Smith", result[1]);
            Assert.Equal("Smith John", result[2]);
        }

        #endregion
    }
}