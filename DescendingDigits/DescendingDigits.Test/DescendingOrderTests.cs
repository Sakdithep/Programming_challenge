using Xunit;

namespace DescendingDigits.Test
{
    public class DescendingOrderTests
    {
        #region Functionality Tests

        [Theory]
        [InlineData(3008, 8300)]
        [InlineData(1989, 9981)]
        [InlineData(2679, 9762)]
        [InlineData(9163, 9631)]
        public void DescendingOrder_ExamplesFromProblem_ReturnsCorrectResult(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(54321, 54321)]
        [InlineData(12345, 54321)]
        [InlineData(24531, 54321)]
        [InlineData(98765, 98765)]
        [InlineData(13579, 97531)]
        [InlineData(24680, 86420)]
        public void DescendingOrder_AllDifferentDigits_SortsCorrectly(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }

        #endregion

        #region Input Validation Tests
        [Theory]
        [InlineData(-1)]
        [InlineData(-42)]
        [InlineData(-100)]
        [InlineData(-1989)]
        [InlineData(-999999)]
        [InlineData(int.MinValue)]
        public void DescendingOrder_NegativeNumber_ThrowsArgumentOutOfRangeException(int number)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                NumberHelper.DescendingOrder(number);
            });

            Assert.Equal(nameof(number), exception.ParamName);
            Assert.Contains("The number must be a positive value.", exception.Message);
        }

        [Fact]
        public void DescendingOrder_Zero_ReturnsZero()
        {
            int input = 0;
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(0, result);
        }
        #endregion

        #region Overflow Tests
        [Fact]
        public void DescendingOrder_ResultExceedsIntMax_ThrowsOverflowException()
        {
            int input = int.MaxValue;

            var exception = Assert.Throws<OverflowException>(() =>
            {
                NumberHelper.DescendingOrder(input);
            });

            Assert.Contains("Result value (8776444321) exceeds the maximum allowed integer value", exception.Message);
        }

        [Theory]
        [InlineData(1000000000, 1000000000)]
        [InlineData(999999999, 999999999)]
        [InlineData(2147483646, 8764443221)]
        public void DescendingOrder_LargeNumbersWithinRange_WorksCorrectly(int input, long expected)
        {
            if (expected > int.MaxValue)
            {
                Assert.Throws<OverflowException>(() =>
                {
                    NumberHelper.DescendingOrder(input);
                });
            }
            else
            {
                int result = NumberHelper.DescendingOrder(input);
                Assert.Equal((int)expected, result);
            }
        }
        #endregion

        #region Edge Cases - Special Numbers
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        [InlineData(6, 6)]
        [InlineData(7, 7)]
        [InlineData(8, 8)]
        [InlineData(9, 9)]
        public void DescendingOrder_SingleDigit_ReturnsSameNumber(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(21, 21)]
        [InlineData(321, 321)]
        [InlineData(4321, 4321)]
        [InlineData(54321, 54321)]
        [InlineData(987654321, 987654321)]
        public void DescendingOrder_AlreadySorted_ReturnsSameNumber(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(12, 21)]
        [InlineData(123, 321)]
        [InlineData(1234, 4321)]
        [InlineData(12345, 54321)]
        [InlineData(123456789, 987654321)]
        public void DescendingOrder_AscendingOrder_ReversesCompletely(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }
        #endregion

        #region Repeated Digits Tests
        [Theory]
        [InlineData(11, 11)]
        [InlineData(111, 111)]
        [InlineData(1111, 1111)]
        [InlineData(22222, 22222)]
        [InlineData(777777, 777777)]
        [InlineData(999999999, 999999999)]
        public void DescendingOrder_AllSameDigits_ReturnsSameNumber(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(112, 211)]
        [InlineData(1122, 2211)]
        [InlineData(1123, 3211)]
        [InlineData(11223, 32211)]
        [InlineData(445566, 665544)]
        [InlineData(121212, 222111)]
        public void DescendingOrder_SomeRepeatedDigits_SortsCorrectly(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }
        #endregion

        #region Numbers with Zeros
        [Theory]
        [InlineData(10, 10)]
        [InlineData(100, 100)]
        [InlineData(1000, 1000)]
        [InlineData(1001, 1100)]
        [InlineData(1020, 2100)]
        [InlineData(10203, 32100)]
        [InlineData(50607, 76500)]
        public void DescendingOrder_NumbersWithZeros_SortsZerosToEnd(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1000, 1000)]
        [InlineData(10000, 10000)]
        [InlineData(10203000, 32100000)]
        [InlineData(900050, 950000)]
        public void DescendingOrder_MultipleZeros_KeepsAllZerosAtEnd(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(102, 210)]
        [InlineData(1002, 2100)]
        [InlineData(10002, 21000)]
        public void DescendingOrder_ResultStartsWithNonZero_Works(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }
        #endregion

        #region Two Digit Numbers
        [Theory]
        [InlineData(10, 10)]
        [InlineData(21, 21)]
        [InlineData(12, 21)]
        [InlineData(34, 43)]
        [InlineData(56, 65)]
        [InlineData(78, 87)]
        [InlineData(90, 90)]
        [InlineData(99, 99)]
        public void DescendingOrder_TwoDigitNumbers_SortsCorrectly(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }
        #endregion

        #region Three Digit Numbers
        [Theory]
        [InlineData(100, 100)]
        [InlineData(123, 321)]
        [InlineData(321, 321)]
        [InlineData(456, 654)]
        [InlineData(789, 987)]
        [InlineData(999, 999)]
        [InlineData(505, 550)]
        [InlineData(707, 770)]
        public void DescendingOrder_ThreeDigitNumbers_SortsCorrectly(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }
        #endregion

        #region Large Numbers
        [Theory]
        [InlineData(123456, 654321)]
        [InlineData(1234567, 7654321)]
        [InlineData(12345678, 87654321)]
        [InlineData(123456789, 987654321)]
        [InlineData(987654321, 987654321)]
        [InlineData(111111111, 111111111)]
        public void DescendingOrder_LargeNumbers_SortsCorrectly(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }
        #endregion

        #region Pattern Tests
        [Theory]
        [InlineData(121, 211)]
        [InlineData(1212, 2211)]
        [InlineData(12121, 22111)]
        [InlineData(121212, 222111)]
        [InlineData(131313, 333111)]
        [InlineData(242424, 444222)]
        public void DescendingOrder_AlternatingPattern_SortsCorrectly(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(11, 11)]
        [InlineData(121, 211)]
        [InlineData(1221, 2211)]
        [InlineData(12321, 32211)]
        [InlineData(123321, 332211)]
        public void DescendingOrder_Palindrome_SortsCorrectly(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
        }
        #endregion

        #region Idempotency Tests
        [Theory]
        [InlineData(3008)]
        [InlineData(1989)]
        [InlineData(12345)]
        [InlineData(99887766)]
        public void DescendingOrder_CalledTwice_ReturnsSameResult(int input)
        {
            int result1 = NumberHelper.DescendingOrder(input);
            int result2 = NumberHelper.DescendingOrder(result1);
            Assert.Equal(result1, result2);
        }
        #endregion

        #region Mathematical Properties
        [Theory]
        [InlineData(12, 21)]
        [InlineData(123, 321)]
        [InlineData(1234, 4321)]
        [InlineData(12345, 54321)]
        public void DescendingOrder_ResultHasSameDigitCount(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
            int inputDigits = input.ToString().Length;
            int resultDigits = result.ToString().Length;
            Assert.Equal(inputDigits, resultDigits);
        }

        [Theory]
        [InlineData(3008, 8300)]
        [InlineData(1989, 9981)]
        [InlineData(12345, 54321)]
        public void DescendingOrder_SumOfDigitsRemainsSame(int input, int expected)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.Equal(expected, result);
            int inputSum = SumOfDigits(input);
            int resultSum = SumOfDigits(result);
            Assert.Equal(inputSum, resultSum);
        }

        private int SumOfDigits(int number)
        {
            int sum = 0;
            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return sum;
        }

        [Theory]
        [InlineData(3008)]
        [InlineData(1989)]
        [InlineData(12345)]
        [InlineData(54321)]
        [InlineData(11111)]
        public void DescendingOrder_ResultIsGreaterOrEqual(int input)
        {
            int result = NumberHelper.DescendingOrder(input);
            Assert.True(result >= input,
                $"Result {result} should be greater than or equal to input {input}");
        }
        #endregion
    }
}