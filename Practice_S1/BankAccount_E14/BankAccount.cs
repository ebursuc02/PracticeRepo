namespace BankAccount_E14;

public class BankAccount
{
    public required string AccountNumber { get; init; }
    public double Balance { get; private set; }

    public void Deposit(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Negative amount.");

        Balance += amount;
        Console.WriteLine($"** Deposited {amount}. Balance: {Balance} **");
    }

    public void Withdraw(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Negative amount.");

        if(amount > Balance)
        {
            Console.WriteLine("Insufficient funds.");
            return;
        }

        Balance -= amount;
        Console.WriteLine($"** Withdrawn {amount}. Balance: {Balance} **");
    }

}
