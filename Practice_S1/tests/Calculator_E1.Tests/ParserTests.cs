using Calculator;

namespace Calculator_E1.Tests;

public class ParserTests
{
    private double Eval(string expression)
    {
        var parser = new ExpressionParser(expression);
        var tree = parser.Parse();
        return tree.Evaluate();
    }


    [Theory]
    [InlineData("3 + 2", 5)]
    [InlineData("10 - 3", 7)]
    [InlineData("3 * 5", 15)]
    [InlineData("8/4", 2)]
    [InlineData("5 % 2", 1)]
    [InlineData("3", 3)]
    public void Test_Simple_Operations(string expression, int expected)
    {
        Assert.Equal(expected, Eval(expression));
    }


    [Theory]
    [InlineData("3 + 2 * 5 + 1", 14)]
    [InlineData("(3 + 2) * (5 + 1)", 30)]
    [InlineData("3 * 2 / 6 % 2 + 1", 2)]
    public void Test_Operator_Precedence(string expression, int expected)
    {
        Assert.Equal(expected, Eval(expression));
    }


    [Theory]
    [InlineData("((3 + (1 + 1)) + 4)", 9)]
    [InlineData("((2 + 3) * (4 + 1))", 25)]
    public void Test_Nested_Parentheses(string expression, int expected)
    {
        Assert.Equal(expected, Eval(expression));
    }

    [Theory]
    [InlineData("5 + a")]
    [InlineData("5 + *")]
    public void Test_Unexpected_Operand(string expression)
    {
        var exception = Assert.Throws<Exception>(() => Eval(expression));
        Assert.StartsWith("Unexpected character", exception.Message);
    }

    [Fact]
    public void Test_Invalid_Parentheses()
    {
        var exception = Assert.Throws<Exception>(() => Eval("(3 + 5"));
        Assert.StartsWith("No closing parenthesis at", exception.Message);

        exception = Assert.Throws<Exception>(() => Eval("3 + 5)"));
        Assert.StartsWith("Unexpected character", exception.Message);
    }
}
