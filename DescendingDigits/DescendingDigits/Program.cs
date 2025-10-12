using System;

namespace DescendingDigits
{
    public static class NumberHelper
    {
        public static int DescendingOrder(int number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(number),
                    number,
                    "The number must be a positive value.");
            }

            if (number == 0) return 0;
            var digits = ExtractDigits(number);
            Array.Sort(digits);
            Array.Reverse(digits);
            return CombineDigits(digits);
        }
        private static int[] ExtractDigits(int number)
        {
            if (number == 0)
                return new int[] { 0 };

            int digitCount = (int)Math.Floor(Math.Log10(number)) + 1;
            int[] digits = new int[digitCount];

            int index = 0;

            while (number > 0)
            {
                digits[index] = number % 10;
                number = number / 10;
                index++;
            }

            return digits;
        }
        private static int CombineDigits(int[] digits)
        {
            long result = 0;
            foreach (int digit in digits)
            {
                result = result * 10 + digit;

                if (result > int.MaxValue)
                {
                    throw new OverflowException(
                        $"Result value ({result}) exceeds the maximum allowed integer value ({int.MaxValue}).");
                }

            }
            return (int)result;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            int[] testCases = { 3008, 1989, 2679, 9163 };
            int[] expectedResults = { 8300, 9981, 9762, 9631 };

            for (int i = 0; i < testCases.Length; i++)
            {
                int input = testCases[i];
                int expected = expectedResults[i];
                int result = NumberHelper.DescendingOrder(input); ;
                Console.WriteLine($"{input} => {result} (Expected : {expected})");
            }
        }
    }
}
