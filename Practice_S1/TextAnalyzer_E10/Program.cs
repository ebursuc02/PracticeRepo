using TextAnalyzer_E10;

Console.WriteLine("Introduceti un text:");
string input = Console.ReadLine();

if (string.IsNullOrWhiteSpace(input))
{
    Console.WriteLine("Eroare: Textul introdus este gol.");
    return;
}

TextAnalyzer.AnalyzeText(input);