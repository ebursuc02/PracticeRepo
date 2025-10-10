namespace TextAnalyzer_E10;
public static class TextAnalyzer
{
    private static readonly char[] _vowels =
        { 'a', 'ă', 'â', 'e', 'i', 'î', 'o', 'u',
          'A', 'Ă', 'Â', 'E', 'I', 'Î', 'O', 'U' };

    public static void AnalyzeText(string text)
    {
        try
        {
            int characters = text.Length;
            int words = text.Split(new[]{ ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;

            int vowels = text.Count(c => _vowels.Contains(c));
            int consonants = text.Count(c =>
                char.IsLetter(c) &&
                !_vowels.Contains(c));

            Console.WriteLine($"- Nr. caractere: {characters} -");
            Console.WriteLine($"- Nr. cuvinte: {words} -");
            Console.WriteLine($"- Nr. vocale: {vowels} -");
            Console.WriteLine($"- Nr. consoane: {consonants} -");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }
}
