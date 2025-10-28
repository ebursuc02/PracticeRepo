using NumberGuesser_E7;

int @base;
string input;

do
{
    Console.WriteLine("\nEnter the base (2-16): ");
    input = Console.ReadLine();

} while (!int.TryParse(input, out @base) || @base < 2 || @base > 16);

var guesser = new NumberGuesser(@base);
guesser.Start();
