using Calculator_E1.Operators.Abstractions;
using Calculator_E1.Operators.Binary;
using Calculator_E1.Operators.Unary;

namespace Calculator_E1.Operators.Registry;

public sealed class OperatorRegistry
{
    private readonly Dictionary<string, IOperator> _ops = new(StringComparer.Ordinal);

    public OperatorRegistry Register(IOperator op)
    {
        _ops[op.Symbol] = op;
        return this;
    }

    public bool TryGet(string symbol, out IOperator op) => _ops.TryGetValue(symbol, out op!);

    public T Get<T>(string symbol) where T : class, IOperator =>
        _ops.TryGetValue(symbol, out var op) && op is T t
            ? t
            : throw new InvalidOperationException($"Operator not found or wrong arity: '{symbol}'");

    public static OperatorRegistry Default => new OperatorRegistry()
        .Register(new AddOperator())
        .Register(new SubOperator())
        .Register(new MulOperator())
        .Register(new DivOperator())
        .Register(new ModOperator())
        .Register(new NegateOperator());
}
