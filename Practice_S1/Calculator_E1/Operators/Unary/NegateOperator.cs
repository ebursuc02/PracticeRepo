using Calculator_E1.Operators.Abstractions;

namespace Calculator_E1.Operators.Unary;

public sealed class NegateOperator : IUnaryOperator
{
    public string Symbol => "neg";
    public int Precedence => 30;
    public Assoc Associativity => Assoc.Right;
    public double Apply(double x) => -x;
}
