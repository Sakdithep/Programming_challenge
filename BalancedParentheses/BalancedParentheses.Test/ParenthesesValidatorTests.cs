using Xunit;

namespace BalancedParentheses.Tests
{
    public class ParenthesesValidatorTests
    {
        [Theory]
        [InlineData("()", true)]
        [InlineData("([])", true)]
        [InlineData("{[()]}", true)]
        [InlineData("({[]})", true)]
        [InlineData("{[()][]({[]})}", true)]
        [InlineData("{[()]}[({})]({[]})", true)]
        [InlineData("[({({[({[]})]})})]", true)]
        [InlineData("{[({[]})]({[]})}", true)]
        [InlineData("{([([])])({[]})[()()]}", true)]
        [InlineData("[{()}([]){{([])}}]", true)]
        [InlineData("(((((((((())))))))))", true)]
        [InlineData("{([({[({[()]})]})])}", true)]
        [InlineData("{[({[({[({[()]})]})]})]}", true)]
        [InlineData("(]", false)]
        [InlineData("([)]", false)]
        [InlineData("(", false)]
        [InlineData(")", false)]
        [InlineData("abc", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("({[})", false)]
        [InlineData("(a)", false)]
        [InlineData("(123)", false)]
        [InlineData("123", false)]
        [InlineData("( )", false)]
        [InlineData(" ()", false)]
        [InlineData("<>", false)]
        [InlineData("(\n)", false)]
        [InlineData("{[()]} ", false)]
        [InlineData("[({)}]", false)]
        [InlineData("{[(])}", false)]
        [InlineData("{[({[({[({[()]})]})])}", false)]
        [InlineData("{[({[({[({[()]})]})]})]]", false)]
        [InlineData("{[({[({[({[()]}])})]})]}", false)]
        [InlineData("[{()}([]){([)]}]", false)]
        public void IsBalanced_ShouldReturnExpectedResult(string input, bool expected)
        {
            var result = ParenthesesValidator.IsBalanced(input);
            Assert.Equal(expected, result);
        }
    }
}
