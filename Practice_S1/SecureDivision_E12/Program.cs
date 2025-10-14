try
{
    Console.Write("Enter the first number: ");
    decimal a = Convert.ToDecimal(Console.ReadLine());

    Console.Write("Enter the second number: ");
    decimal b = Convert.ToDecimal(Console.ReadLine());

    if (b == 0)
    {
        Console.WriteLine("Error: division by 0 not allowed.");
        return;
    }

    decimal result = a / b;

    result = Math.Round(result, 2, MidpointRounding.ToEven);

    Console.WriteLine($"Result a/b: {result}");
}
catch (FormatException)
{
    Console.WriteLine("Error: number formats are incorrect.");
}