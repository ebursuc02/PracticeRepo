using StockTracking_E15;

var stock = new Stock();

var product1 = new Product("Laptop X1", "Lenovo", "Electronics", 1200.0, 5);
var product2 = new Product("Mouse MX Master 3", "Logitech", "Electronics", 100.0, 10);
var product3 = new Product("Office Chair", "IKEA", "Furniture", 250.0, 4);
var product4 = new Product("Desk Lamp", "Philips", "Furniture", 60.0, 6);
var product5 = new Product("Water Bottle", "Contigo", "Accessories", 25.0, 15);

try
{
    stock.AddProduct(product1);
    stock.AddProduct(product2);
    stock.AddProduct(product3);
    stock.AddProduct(product4);
    stock.AddProduct(product5);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


stock.PrintStockValue();