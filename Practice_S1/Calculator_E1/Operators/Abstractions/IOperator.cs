namespace Calculator_E1.Operators.Abstractions;

public enum OperatorAssociativity { Left, Right }

public interface IOperator
{
    string Symbol { get; }
    int Precedence { get; }                       // for parsing (weights)
    OperatorAssociativity Associativity { get; }  // for parsing (left/right)
}
