using Calculator_E1.Operators.Abstractions;

namespace Calculator_E1.Operators.Binary;

public sealed class AddOperator : IBinaryOperator
{
    public string Symbol => "+";
    public int Precedence => 10;
    public Assoc Associativity => Assoc.Left;
    public double Apply(double l, double r) => l + r;
}
