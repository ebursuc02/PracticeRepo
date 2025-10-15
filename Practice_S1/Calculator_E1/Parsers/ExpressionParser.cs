using Calculator_E1.Domain;
using Calculator_E1.Operators.Abstractions;
using Calculator_E1.Operators.Registry;
using System.Text;

namespace Calculator_E1.Parsers;

public sealed class ExpressionParser
{
    private readonly OperatorRegistry _registry;

    private int _index = 0;
    private string _expression = string.Empty;

    private char Current => _index < _expression.Length ? _expression[_index] : char.MinValue;

    public ExpressionParser(OperatorRegistry? registry = null) => _registry = registry ?? OperatorRegistry.Default;

    public Expression Parse(string input)
    {
        if (input is not null)
        {
            _expression = input.Replace(" ", "");
            _index = 0;

            if (_expression.Length == 0)
                throw new Exception("Empty expression.");

            var tree = ParseExpression(minPrec: 0);

            if (Current != char.MinValue)
                throw new Exception($"Unexpected character at position {_index}: '{Current}'");

            return tree;
        }

        throw new ArgumentNullException(nameof(input));
    }

    private Expression ParseExpression(int minPrec)
    {
        var left = ParsePrimaryWithUnary();

        while (TryPeekBinary(out var bin))
        {
            if (bin.Precedence < minPrec) break;

            _index++;

            var nextMin = bin.Associativity == Assoc.Left ? bin.Precedence + 1 : bin.Precedence;
            var right = ParseExpression(nextMin);

            left = new Expression
            {
                Operator = bin,
                LeftOperand = left,
                RightOperand = right
            };
        }

        return left;
    }

    private Expression ParsePrimaryWithUnary()
    {
        if (Current == '-')
        {
            _index++;
            var operand = ParsePrimary();

            if (_registry.TryGet("neg", out var op) && op is IUnaryOperator unary)
            {
                return new Expression { Operator = unary, RightOperand = operand };
            }

            // fallback: 0 - operand
            var minus = _registry.Get<IBinaryOperator>("-");
            return new Expression
            {
                Operator = minus,
                LeftOperand = new Expression { Value = 0 },
                RightOperand = operand
            };
        }

        return ParsePrimary();
    }

    private Expression ParsePrimary()
    {
        if (char.IsDigit(Current))
            return ParseNumber();

        if (Current == '(')
        {
            _index++;
            var inner = ParseExpression(0);
            if (Current != ')')
                throw new Exception($"No closing parenthesis at position {_index}");
            _index++;
            return inner;
        }

        throw new Exception(Current == char.MinValue
            ? "Unexpected end of input"
            : $"Unexpected character: {Current}");
    }

    private Expression ParseNumber()
    {
        var number = new StringBuilder();
        while (char.IsDigit(Current) || Current == '.')
        {
            number.Append(Current);
            _index++;
        }
        return new Expression { Value = double.Parse(number.ToString()) };
    }

    private bool TryPeekBinary(out IBinaryOperator binary)
    {
        binary = default!;
        if (Current == char.MinValue) return false;

        var symbol = Current.ToString(); // single-char ops
        if (_registry.TryGet(symbol, out var op) && op is IBinaryOperator b)
        {
            binary = b;
            return true;
        }
        return false;
    }
}
