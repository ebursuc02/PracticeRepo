using Calculator_E1.Parsers;

var parser = new ExpressionParser();

var readInput = () =>
{
    Console.WriteLine("Write the expression using following operators (or Enter to exit): +, -, /, %, *");
    return Console.ReadLine() ?? string.Empty;
};

while (true)
{
    var input = readInput();
    if (string.IsNullOrWhiteSpace(input)) break;

    try
    {
        var expr = parser.Parse(input);
        var value = expr.Evaluate();
        Console.WriteLine($"= {value}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

