using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator;

public class ExpressionParser
{
    private int _index = 0;
    private string _expression;
    private char Current => _index < _expression.Length ? _expression[_index] : char.MinValue;
    public ExpressionParser(string expression) => _expression = expression.Replace(" ", "");

    public Expression Parse()
    {

        var tree = ParseAddSub();
        if (Current != char.MinValue)
            throw new Exception($"Unexpected character at position {_index}");
        return tree;

    }

    public Expression ParseAddSub()
    {
        var node = ParseMulDivMod();
        while (Current == '-' || Current == '+')
        {
            char op = Current;
            _index++;
            var right = ParseMulDivMod();
            node = new Expression { LeftOperand = node, RightOperand = right, Operator = op };
        }
        return node;
    }

    public Expression ParseMulDivMod()
    {
        var node = ParseNumber();
        while (Current == '*' || Current == '/' || Current == '%')
        {
            char op = Current;
            _index++;
            var right = ParseNumber();
            node = new Expression { LeftOperand = node, RightOperand = right, Operator = op };
        }
        return node;
    }

    public Expression ParseNumber()
    {
        if ( char.IsDigit(Current) )
        {
            var number = new StringBuilder();
            while (char.IsDigit(Current) || Current == '.')
            {
                number.Append(Current);
                _index++;
            }
            return new Expression { Value = double.Parse(number.ToString()) };
        }
        if ( Current == '(' )
        {
            _index++;
            var node = ParseAddSub();
            if (Current != ')') throw new Exception($"No closing parenthesis at {_index}");
            _index++;
            return node;
        }
        throw new Exception($"Unexpected character: {Current}");
    }
}
