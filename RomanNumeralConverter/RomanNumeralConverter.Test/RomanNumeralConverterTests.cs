using Xunit;

namespace RomanNumeralConverter.Tests
{
    public class RomanNumeralConverterTests
    {
        #region Number To Roman Tests
        [Theory]
        [InlineData(1, "I")]
        [InlineData(5, "V")]
        [InlineData(10, "X")]
        [InlineData(50, "L")]
        [InlineData(100, "C")]
        [InlineData(500, "D")]
        [InlineData(1000, "M")]
        public void NumberToRoman_BasicNumbers_ReturnsCorrectRoman(int number, string expected)
        {
            var result = RomanNumeralConverter.NumberToRoman(number);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(4, "IV")]
        [InlineData(9, "IX")]
        [InlineData(40, "XL")]
        [InlineData(90, "XC")]
        [InlineData(400, "CD")]
        [InlineData(900, "CM")]
        public void NumberToRoman_SubtractiveNotation_ReturnsCorrectRoman(int number, string expected)
        {
            var result = RomanNumeralConverter.NumberToRoman(number);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, "II")]
        [InlineData(3, "III")]
        [InlineData(6, "VI")]
        [InlineData(7, "VII")]
        [InlineData(8, "VIII")]
        [InlineData(11, "XI")]
        [InlineData(12, "XII")]
        [InlineData(13, "XIII")]
        [InlineData(14, "XIV")]
        [InlineData(15, "XV")]
        [InlineData(16, "XVI")]
        [InlineData(17, "XVII")]
        [InlineData(18, "XVIII")]
        [InlineData(19, "XIX")]
        [InlineData(20, "XX")]
        public void NumberToRoman_CompoundNumbers_ReturnsCorrectRoman(int number, string expected)
        {
            var result = RomanNumeralConverter.NumberToRoman(number);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(58, "LVIII")]        // 50 + 5 + 3
        [InlineData(1994, "MCMXCIV")]    // 1000 + 900 + 90 + 4
        [InlineData(2023, "MMXXIII")]    // 2000 + 20 + 3
        [InlineData(3444, "MMMCDXLIV")]  // 3000 + 400 + 40 + 4
        [InlineData(3999, "MMMCMXCIX")]  // ค่าสูงสุดที่รองรับ
        public void NumberToRoman_ComplexNumbers_ReturnsCorrectRoman(int number, string expected)
        {
            var result = RomanNumeralConverter.NumberToRoman(number);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, "I")]             // ค่าต่ำสุดที่รองรับ
        [InlineData(3999, "MMMCMXCIX")]  // ค่าสูงสุดที่รองรับ
        public void NumberToRoman_BoundaryValues_ReturnsCorrectRoman(int number, string expected)
        {
            var result = RomanNumeralConverter.NumberToRoman(number);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(int.MinValue)]
        public void NumberToRoman_NumberTooLow_ThrowsArgumentOutOfRangeException(int number)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                RomanNumeralConverter.NumberToRoman(number);
            });

            Assert.Contains("Number must be between", exception.Message);
        }

        [Theory]
        [InlineData(4000)]
        [InlineData(5000)]
        [InlineData(10000)]
        [InlineData(int.MaxValue)]
        public void NumberToRoman_NumberTooHigh_ThrowsArgumentOutOfRangeException(int number)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                RomanNumeralConverter.NumberToRoman(number);
            });

            Assert.Contains("Number must be between", exception.Message);
        }

        [Theory]
        [InlineData(2000, "MM")]
        [InlineData(3000, "MMM")]
        [InlineData(30, "XXX")]
        [InlineData(300, "CCC")]
        public void NumberToRoman_RepeatingSymbols_ReturnsCorrectRoman(int number, string expected)
        {
            var result = RomanNumeralConverter.NumberToRoman(number);
            Assert.Equal(expected, result);
        }
        #endregion

        #region Roman To Number Tests
        [Theory]
        [InlineData("I", 1)]
        [InlineData("V", 5)]
        [InlineData("X", 10)]
        [InlineData("L", 50)]
        [InlineData("C", 100)]
        [InlineData("D", 500)]
        [InlineData("M", 1000)]
        public void RomanToNumber_BasicRoman_ReturnsCorrectNumber(string roman, int expected)
        {
            var result = RomanNumeralConverter.RomanToNumber(roman);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("IV", 4)]
        [InlineData("IX", 9)]
        [InlineData("XL", 40)]
        [InlineData("XC", 90)]
        [InlineData("CD", 400)]
        [InlineData("CM", 900)]
        public void RomanToNumber_SubtractiveNotation_ReturnsCorrectNumber(string roman, int expected)
        {
            var result = RomanNumeralConverter.RomanToNumber(roman);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("II", 2)]
        [InlineData("III", 3)]
        [InlineData("VI", 6)]
        [InlineData("VII", 7)]
        [InlineData("VIII", 8)]
        [InlineData("XI", 11)]
        [InlineData("XII", 12)]
        [InlineData("XIII", 13)]
        [InlineData("XIV", 14)]
        [InlineData("XV", 15)]
        [InlineData("XX", 20)]
        [InlineData("XXX", 30)]
        public void RomanToNumber_CompoundRoman_ReturnsCorrectNumber(string roman, int expected)
        {
            var result = RomanNumeralConverter.RomanToNumber(roman);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("LVIII", 58)]
        [InlineData("MCMXCIV", 1994)]
        [InlineData("MMXXIII", 2023)]
        [InlineData("MMMCDXLIV", 3444)]
        [InlineData("MMMCMXCIX", 3999)]
        public void RomanToNumber_ComplexRoman_ReturnsCorrectNumber(string roman, int expected)
        {
            var result = RomanNumeralConverter.RomanToNumber(roman);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("iv", 4)]
        [InlineData("ix", 9)]
        [InlineData("mcmxciv", 1994)]
        [InlineData("MCMXCIV", 1994)]
        [InlineData("McmXciv", 1994)]
        public void RomanToNumber_MixedCase_ReturnsCorrectNumber(string roman, int expected)
        {
            var result = RomanNumeralConverter.RomanToNumber(roman);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void RomanToNumber_NullInput_ThrowsArgumentNullException()
        {
            string nullRoman = null;

            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                RomanNumeralConverter.RomanToNumber(nullRoman);
            });

            Assert.Contains("null", exception.Message.ToLower());
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t")]
        [InlineData("\n")]
        public void RomanToNumber_EmptyOrWhitespace_ThrowsArgumentNullException(string roman)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                RomanNumeralConverter.RomanToNumber(roman);
            });

            Assert.Contains("Roman numeral must not be null or empty", exception.Message);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("ABC")]
        [InlineData("IXZ")]
        [InlineData("MCMXCIVB")]
        [InlineData("123")]
        [InlineData("IV123")]
        [InlineData("!@#$")]
        public void RomanToNumber_InvalidCharacters_ThrowsArgumentException(string roman)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                RomanNumeralConverter.RomanToNumber(roman);
            });

            Assert.Contains("Invalid Roman numeral character", exception.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(9)]
        [InlineData(58)]
        [InlineData(1994)]
        [InlineData(3999)]
        public void RomanToNumber_RoundTrip_MaintainsValue(int originalNumber)
        {
            var roman = RomanNumeralConverter.NumberToRoman(originalNumber);
            var resultNumber = RomanNumeralConverter.RomanToNumber(roman);
            Assert.Equal(originalNumber, resultNumber);
        }

        [Theory]
        [InlineData("I", 1)]             // ค่าต่ำสุด
        [InlineData("MMMCMXCIX", 3999)]  // ค่าสูงสุด
        public void RomanToNumber_BoundaryValues_ReturnsCorrectNumber(string roman, int expected)
        {
            var result = RomanNumeralConverter.RomanToNumber(roman);
            Assert.Equal(expected, result);
        }
        #endregion

        #region Integration Tests
        [Theory]
        [InlineData(1, 100)]
        [InlineData(100, 500)]
        [InlineData(500, 1000)]
        [InlineData(1000, 2000)]
        public void Integration_RoundTripConversion_MaintainsAllValues(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                var roman = RomanNumeralConverter.NumberToRoman(i);
                var number = RomanNumeralConverter.RomanToNumber(roman);

                Assert.Equal(i, number);
            }
        }
        #endregion
    }
}