namespace Calculator;

public class Expression
{
    public double Value { get; set; }
    public char Operator { get; init; }
    public Expression RightOperand { get; init; }
    public Expression LeftOperand { get; init; }

    public double Evaluate() => Operator switch
    {
        '+' => LeftOperand.Evaluate() + RightOperand.Evaluate(),
        '-' => LeftOperand.Evaluate() - RightOperand.Evaluate(),
        '*' => LeftOperand.Evaluate() * RightOperand.Evaluate(),
        '/' => LeftOperand.Evaluate() / RightOperand.Evaluate(),
        '%' => LeftOperand.Evaluate() % RightOperand.Evaluate(),
        _ => Value != double.NaN ? Value : throw new InvalidOperationException($"Unknown operation: {Operator}")
    };
            
}
