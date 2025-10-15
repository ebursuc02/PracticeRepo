namespace Calculator_E1.Operators.Abstractions;

public enum Assoc { Left, Right }

public interface IOperator
{
    string Symbol { get; }
    int Precedence { get; }          // for parsing (weights)
    Assoc Associativity { get; }     // for parsing (left/right)
}
