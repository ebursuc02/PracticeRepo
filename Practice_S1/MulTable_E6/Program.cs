
int number;
string input;

do {
    Console.Write("\nEnter a number in range 1-10: ");
    input = Console.ReadLine();
} while (!int.TryParse(input, out number) || number < 1 || number > 10) ;

for( int i = 1; i <= 10; i++ ) 
    Console.WriteLine($"{number} x {i} = {number * i}");