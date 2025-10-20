using Calculator_E1.Operators.Abstractions;

namespace Calculator_E1.Operators.Binary;

public sealed class SubOperator : IBinaryOperator
{
    public string Symbol => "-";
    public int Precedence => 10;
    public OperatorAssociativity Associativity => OperatorAssociativity.Left;
    public double Apply(double l, double r) => l - r;
}
