namespace NumberGuesser_E7;

internal class NumberGuesser
{
    private readonly int _base;
    private int _tries;

    public NumberGuesser(int @base) => _base = @base;

    public void Start()
    {
        int min = 0, max = 100;
        string response = string.Empty;
        _tries = 0;

        Console.WriteLine($"Give a number in range {min}-{max} in base {_base}. I will try to guess it!");

        do
        {
            int guess = (min + max) / 2;
            string nrInBase = NumberConvertor.ToBase(guess, _base);
            _tries++;

            Console.WriteLine($"Is your number {nrInBase} ({guess}) ? (smaller/greater/correct)");
            response = Console.ReadLine()?.Trim().ToLower();

            if (response == "smaller") max = guess;
            else if(response == "greater") min = guess;

        } while (response != "correct");

        Console.WriteLine($"** Guessed the number in {_tries} attempts **");
    }
}
