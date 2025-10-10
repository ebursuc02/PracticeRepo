namespace StockTracking_E15;

public class Product
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public int Quantity { get; private set; }

    public Product(string name, string brand, string category, double price, int quantity)
    {
        if (price <= 0 || quantity <= 0)
            throw new ArgumentException("The price and quantity should have positive values.");

        Name = name;
        Brand = brand;
        Category = category;
        Price = price;
        Quantity = quantity;
    }

    public void AddStock(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("The amount should have positive value.");

        Quantity += amount;
    }

    public void RemoveStock(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("The amount should have positive value.");
        if (amount > Quantity)
            throw new ArgumentException("Not enough products in stock.");

        Quantity -= amount;
    }
}
