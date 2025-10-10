namespace NumberGuesser_E7;

public static class NumberConvertor
{
    public static string ToBase(int number, int @base)
    {
        const string digits = "0123456789ABCDEF";
        if (number == 0) return "0";
        string result = string.Empty;
        while( number > 0 )
        {
            result = digits[number % @base] + result;
            number /= @base;
        }
        return result;
    }
    public static int FromBase(string number, int @base)
    {
        const string digits = "0123456789ABCDEF";
        int result = 0;
        foreach( char c in number )
        {
            result = result * @base + digits.IndexOf(char.ToUpper(c));
        }
        return result;
    }
}
