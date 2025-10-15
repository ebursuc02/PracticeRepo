namespace Calculator_E1.Operators.Abstractions;

public interface IBinaryOperator : IOperator
{
    double Apply(double left, double right);
}
