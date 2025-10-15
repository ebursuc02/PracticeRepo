using Calculator_E1.Operators.Abstractions;

namespace Calculator_E1.Domain;

public class Expression
{
    public double? Value { get; init; }
    public IOperator? Operator { get; init; }
    public Expression? LeftOperand { get; init; }
    public Expression? RightOperand { get; init; }

    public double Evaluate() => this switch
    {
        { Operator: null, Value: double value } => value,

        { Operator: IBinaryOperator b, LeftOperand: { } l, RightOperand: { } r }
            => b.Apply(l.Evaluate(), r.Evaluate()),

        { Operator: IUnaryOperator u, RightOperand: { } r }
            => u.Apply(r.Evaluate()),

        _ => throw new InvalidOperationException("Invalid expression shape.")
    };
}
