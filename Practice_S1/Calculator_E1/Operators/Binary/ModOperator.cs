using Calculator_E1.Operators.Abstractions;

namespace Calculator_E1.Operators.Binary;

public sealed class ModOperator : IBinaryOperator
{
    public string Symbol => "%";
    public int Precedence => 20;
    public OperatorAssociativity Associativity => OperatorAssociativity.Left;
    public double Apply(double l, double r) => l % r;
}
