using Calculator;

Console.WriteLine("Write the expression using following operators: +, -, /, %, *");
string expression = Console.ReadLine();

try
{
    var parser = new ExpressionParser(expression);
    Expression tree = parser.Parse();
    double result = tree.Evaluate();
    Console.WriteLine($"Result: {result}");
}
catch( Exception ex )
{
    Console.WriteLine($"Error: {ex.Message}");
}