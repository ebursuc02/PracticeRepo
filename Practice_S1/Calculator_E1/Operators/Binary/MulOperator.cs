using Calculator_E1.Operators.Abstractions;

namespace Calculator_E1.Operators.Binary;

public sealed class MulOperator : IBinaryOperator
{
    public string Symbol => "*";
    public int Precedence => 20;
    public Assoc Associativity => Assoc.Left;
    public double Apply(double l, double r) => l * r;
}
