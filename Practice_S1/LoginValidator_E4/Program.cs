var tries = 3;

while( tries != 0)
{
    Console.WriteLine("\nEnter your username:");
    var username = Console.ReadLine();

    if( username != "username" )
    {
        tries--;
        Console.WriteLine($"Err: The username is not valid. You have {tries} more tries.");
        continue;
    }

    Console.WriteLine("\nEnter your password:");
    var password = Console.ReadLine();

    while( password != "password" )
    {
        tries--;
        Console.WriteLine($"Err: The password is not valid. You have {tries} more tries.");
        if ( tries == 0 ) return;
        Console.WriteLine("\nEnter your password:");
        password = Console.ReadLine();
    }

    Console.WriteLine("** SUCCESS **");
    return;
    
}