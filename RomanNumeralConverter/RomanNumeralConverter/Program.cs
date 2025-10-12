using System.Text;

namespace RomanNumeralConverter
{
    public static class RomanNumeralConverter
    {
        public static readonly Dictionary<char, int> RomanNumberDictionary;
        public static readonly Dictionary<int, string> NumberRomanDictionary;

        private const int MinRomanValue = 1;
        private const int MaxRomanValue = 3999;

        static RomanNumeralConverter()
        {
            RomanNumberDictionary = new Dictionary<char, int>
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 },
            };

            NumberRomanDictionary = new Dictionary<int, string>
            {
                { 1000, "M" },
                { 900, "CM" },
                { 500, "D" },
                { 400, "CD" },
                { 100, "C" },
                { 90, "XC" },
                { 50, "L" },
                { 40, "XL" },
                { 10, "X" },
                { 9, "IX" },
                { 5, "V" },
                { 4, "IV" },
                { 1, "I" },
            };
        }

        public static string NumberToRoman(int number)
        {
            if (number < MinRomanValue || number > MaxRomanValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(number),
                    $"Number must be between {MinRomanValue} and {MaxRomanValue}.");
            }

            var roman = new StringBuilder();

            foreach (var item in NumberRomanDictionary)
            {
                while (number >= item.Key)
                {
                    roman.Append(item.Value);
                    number -= item.Key;
                }
            }

            return roman.ToString();
        }

        public static int RomanToNumber(string roman)
        {
            if (string.IsNullOrWhiteSpace(roman))
            {
                throw new ArgumentNullException(
                    nameof(roman),
                    "Roman numeral must not be null or empty");
            }

            roman = roman.ToUpper();

            int total = 0;
            int previousValue = 0;

            for (int i = 0; i < roman.Length; i++)
            {
                char currentRoman = roman[i];

                if (!RomanNumberDictionary.ContainsKey(currentRoman))
                {
                    throw new ArgumentException(
                         $"Invalid Roman numeral character: '{currentRoman}' at position {i}",
                     nameof(roman));
                }

                int currentValue = RomanNumberDictionary[currentRoman];

                if (previousValue > 0 && currentValue > previousValue)
                {
                    total = total - (2 * previousValue) + currentValue;
                }
                else
                {
                    total += currentValue;
                }

                previousValue = currentValue;
            }

            return total;
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int[] testNumbers = { 4, 9, 58, 1994, 3999 };

                foreach (int num in testNumbers)
                {
                    string roman = RomanNumeralConverter.NumberToRoman(num);
                    Console.WriteLine($"{num} => {roman}");
                }

                string[] testRomans = { "IV", "IX", "LVIII", "MCMXCIV", "MMMCMXCIX" };

                foreach (string roman in testRomans)
                {
                    int number = RomanNumeralConverter.RomanToNumber(roman);
                    Console.WriteLine($"{roman} => {number}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid: {ex.Message}");
            }
        }
    }
}