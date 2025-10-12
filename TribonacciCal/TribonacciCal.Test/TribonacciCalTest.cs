using Xunit;

namespace TribonacciCal.Tests
{
    public class TribonacciCal
    {
        [Fact]
        public void TribonacciCal_Example_FromProblem()
        {
            var result1 = Tribonacci.Calculate(new int[] { 1, 3, 5 }, 5);
            Assert.Equal(new int[] { 1, 3, 5, 9, 17 }, result1);

            var result2 = Tribonacci.Calculate(new int[] { 2, 2, 2 }, 3);
            Assert.Equal(new int[] { 2, 2, 2 }, result2);
        }

        [Fact]
        public void TribonacciCal_BasicCase_ThreeInitialValues()
        {
            var result = Tribonacci.Calculate(new int[] { 1, 3, 5 }, 5);
            Assert.Equal(new int[] { 1, 3, 5, 9, 17 }, result);
        }

        [Fact]
        public void TribonacciCal_AllSameValues()
        {
            var result = Tribonacci.Calculate(new int[] { 2, 2, 2 }, 3);
            Assert.Equal(new int[] { 2, 2, 2 }, result);
        }

        [Fact]
        public void TribonacciCal_AllSameValues_Extended()
        {
            var result = Tribonacci.Calculate(new int[] { 2, 2, 2 }, 7);
            Assert.Equal(new int[] { 2, 2, 2, 6, 10, 18, 34 }, result);
        }

        [Fact]
        public void TribonacciCal_EmptyItems()
        {
            var result = Tribonacci.Calculate(new int[] { }, 5);
            Assert.Equal(new int[] { 0, 0, 0, 0, 0 }, result);
        }

        [Fact]
        public void TribonacciCal_OneInitialValue()
        {
            var result = Tribonacci.Calculate(new int[] { 1 }, 5);
            Assert.Equal(new int[] { 1, 1, 2, 4, 7 }, result);
        }

        [Fact]
        public void TribonacciCal_TwoInitialValues()
        {
            var result = Tribonacci.Calculate(new int[] { 3, 4 }, 6);
            Assert.Equal(new int[] { 3, 4, 7, 14, 25, 46 }, result);
        }

        [Fact]
        public void TribonacciCal_WithZeroValues()
        {
            var result = Tribonacci.Calculate(new int[] { 5, 2, 0 }, 6);
            Assert.Equal(new int[] { 5, 2, 0, 7, 9, 16 }, result);
        }

        [Fact]
        public void TribonacciCal_ZeroN()
        {
            var result = Tribonacci.Calculate(new int[] { 1, 2, 3 }, 0);
            Assert.Equal(new int[] { }, result);
        }

        [Fact]
        public void TribonacciCal_N_One()
        {
            var result = Tribonacci.Calculate(new int[] { 3, 4, 1 }, 1);
            Assert.Equal(new int[] { 3 }, result);
        }

        [Fact]
        public void TribonacciCal_N_Two()
        {
            var result = Tribonacci.Calculate(new int[] { 3, 4, 1 }, 2);
            Assert.Equal(new int[] { 3, 4 }, result);
        }

        [Fact]
        public void TribonacciCal_N_EqualsItemsLength()
        {
            var result = Tribonacci.Calculate(new int[] { 1, 2, 3 }, 3);
            Assert.Equal(new int[] { 1, 2, 3 }, result);
        }

        [Fact]
        public void TribonacciCal_LargeSequence()
        {
            var result = Tribonacci.Calculate(new int[] { 0, 0, 1 }, 10);
            Assert.Equal(new int[] { 0, 0, 1, 1, 2, 4, 7, 13, 24, 44 }, result);
        }

        [Fact]
        public void TribonacciCal_AllZeros()
        {
            var result = Tribonacci.Calculate(new int[] { 0, 0, 0 }, 5);
            Assert.Equal(new int[] { 0, 0, 0, 0, 0 }, result);
        }

        [Fact]
        public void TribonacciCal_NegativeNumbers()
        {
            var result = Tribonacci.Calculate(new int[] { -1, 2, 3 }, 6);
            Assert.Equal(new int[] { -1, 2, 3, 4, 9, 16 }, result);
        }

        [Fact]
        public void TribonacciCal_NullItems()
        {
            var result = Tribonacci.Calculate(null, 5);
            Assert.Equal(new int[] { 0, 0, 0, 0, 0 }, result);
        }

        

        [Fact]
        public void TribonacciCal_LargerNumbers()
        {
            var result = Tribonacci.Calculate(new int[] { 10, 20, 30 }, 6);
            Assert.Equal(new int[] { 10, 20, 30, 60, 110, 200 }, result);
        }
    }
}