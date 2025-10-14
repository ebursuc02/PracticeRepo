using System.Windows.Markup;

void GenerateFibonacciSeq(int n)
{
    int count = 2;
    int a = 0, b = 1, c = a + b;
    Console.Write($"{a} {b} ");
    do
    {
        Console.Write($"{c} ");
        a = b;
        b = c;
        c = a + b;
        count++;
    } while (count != n);
}

void GenerateGeometricSeq(int n)
{
    Console.Write("Please enter the ratio: ");
    int ratio = int.Parse(Console.ReadLine());

    Console.Write("Plase enter the first term of the geometric sequence: ");
    int a1 = int.Parse(Console.ReadLine());

    int count = 0;

    do
    {
        Console.Write($"{a1} ");
        a1 *= ratio;
        count++;
    } while(count != n);
    
}

void GeneratePrimeNumSeq(int n)
{
    int nr = 1, count = 0;
    while (count != n)
    {
        if( nr == 1 || nr == 2 ) 
        {
            Console.Write($"{nr} ");
            nr++;
            count++;
            continue;
        } else
        {
            bool isPrime = true;
            for (int i = 3; i * i <= nr; i += 2)
                if (nr % i == 0)
                {
                    isPrime = false;
                    break;
                }
            if(isPrime)
            {
                Console.Write($"{nr} ");
                count++;
            }         
            nr += 2;
        } 
    }
}

void GenerateFactorialSeq(int n)
{
    var factorial = 1;
    for (int i = 1; i <= n; i++)
    {
        factorial *= i;
        Console.Write($"{factorial} ");
    }
}

void GenerateTriangularSeq(int n)
{
    for (int i = 1; i <= n; ++i)
        Console.Write($"{i * (i + 1) / 2} ");
}

void GenerateSquareSeq(int n)
{
    for (int i = 1; i <= n; ++i)
        Console.Write($"{i * i} ");
}

void GeneratePerfectSequence(int n)
{
    int count = 0;
    int nr = 6;
    while( count != n )
    {
        int sum = 0;
        for( int i = 1; i <= nr / 2; i++)
            if (nr % i == 0) sum += i;
        if (sum == nr)
        {
            Console.Write($"{nr} ");
            count++;
        }
        nr += 2;
    }
}



string input = string.Empty;

while(input != "exit")
{
    int number = 0;
    Console.Write("Enter the number of elements in the sequence (or 'exit'): ");
    input = Console.ReadLine();

    if (input == "exit") return;

    number = int.Parse(input);

    if ( number < 0 )
    {
        Console.Write("Enter a positive number of elements in the sequence (or 'exit'): ");
        input = Console.ReadLine();
        continue;
    }

    Console.Write("Enter the type of sequence (fibonacci/geometric/prime/factorial/triangular/square/perfect): ");
    input = Console.ReadLine();

    switch(input)
    {
        case "fibonacci": 
            GenerateFibonacciSeq(number); 
            break;
        case "geometric":
            GenerateGeometricSeq(number); 
            break;
        case "prime":
            GeneratePrimeNumSeq(number); 
            break;
        case "factorial":
            GenerateFactorialSeq(number);
            break;
        case "triangular":
            GenerateTriangularSeq(number);
            break;
        case "square":
            GenerateSquareSeq(number);
            break;
        case "perfect":
            GeneratePerfectSequence(number);
            break;
        default:
            Console.WriteLine("No such sequence.");
            break;
    }

    Console.WriteLine();

}



