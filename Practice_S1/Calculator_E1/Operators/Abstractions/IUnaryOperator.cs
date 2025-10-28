namespace Calculator_E1.Operators.Abstractions;

public interface IUnaryOperator : IOperator
{
    double Apply(double operand);
}
