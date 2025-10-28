using BankAccount_E14;

var account = new BankAccount { AccountNumber = "RO49AAAA1B38819878297878" };
string action = string.Empty;

while (true)
{
    do
    {
        Console.Write("Enter an action (deposit/withdraw) or 'quit' to exit: ");
        action = Console.ReadLine();
        if (action == "quit") return;
    } while (action != "deposit" && action != "withdraw");

    string input = string.Empty;
    double amount;
    do
    {
        Console.Write("Enter a valid value for the amount or 'quit' to exit: ");
        input = Console.ReadLine();
        if (input == "quit") return;
    } while (!double.TryParse(input, out amount));

    try
    {
        if (action == "deposit")
            account.Deposit(amount);
        else
            account.Withdraw(amount);
    } catch (Exception ex) { Console.WriteLine(ex.Message); }
    
}


