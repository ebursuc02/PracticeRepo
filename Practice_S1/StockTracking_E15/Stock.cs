namespace StockTracking_E15;
public class Stock
{
    private readonly List<Product> products = [];
    public void AddProduct(Product product) => products.Add(product);
    public void RemoveProduct(Product product) => products.Remove(product);

    public void PrintStockValue()
    {
        Console.WriteLine("*** Current Stock: ***");
        var groupedProducts = products.GroupBy(p => p.Category);
        foreach (var category in groupedProducts)
        {
            Console.WriteLine($"\n- {category.Key} -");
            foreach (var product in category)
                Console.WriteLine($"{product.Brand}, {product.Name}: {product.Quantity} pcs x {product.Price} = {product.Quantity * product.Price}");
        }
        Console.WriteLine($"\n*** Total Value: {products.Sum(p => p.Price * p.Quantity)} ***");
    }
}
