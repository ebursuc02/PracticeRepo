using Calculator_E1.Operators.Abstractions;

namespace Calculator_E1.Domain;

public class Expression
{
    public double? Value { get; init; }
    public IOperator? Operator { get; init; }
    public Expression? LeftOperand { get; init; }
    public Expression? RightOperand { get; init; }

    public double Evaluate() =>
        Operator switch
        {
            null => Value ?? throw new InvalidOperationException("Leaf has no value."),
            IBinaryOperator b => b.Apply(
                LeftOperand?.Evaluate() ?? throw new InvalidOperationException("Missing left."),
                RightOperand?.Evaluate() ?? throw new InvalidOperationException("Missing right.")
            ),
            IUnaryOperator u => u.Apply(
                RightOperand?.Evaluate() ?? throw new InvalidOperationException("Missing operand.")
            ),
            _ => throw new InvalidOperationException("Unknown operator type.")
        };
}
