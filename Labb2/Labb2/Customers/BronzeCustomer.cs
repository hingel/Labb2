namespace Labb2;

public class BronzeCustomer : Customer
{
    private int _discount = 5;

    public BronzeCustomer(string name, string password) : base(name, password) { }

    public int Discount
    {
        get { return _discount; }
    }

    public override void ListShoppingCart()
    {
        int count = 0;

        foreach (var prod in ShoppingCart)
        {
            count++;
            Console.WriteLine($"{count}. {prod}. \n\t Total sum with your {Discount}% Discount: {(prod.Quantity * prod.Price* (100 - Discount) / 100) / Currency.CurrencyDouble} {Currency.CurrencyName}");
        }

        Console.WriteLine($"Total price: {TotalPrice()}");
        Console.WriteLine("Press any key to continue.");
    }

    public override double TotalPrice()
    {
        double sumTotal = 0;

        foreach (var prod in ShoppingCart)
        {
            sumTotal += prod.Price * prod.Quantity * (100 - Discount)/100;
        }

        return sumTotal;
    }
    public override string ToString()
    {
        return base.ToString() + (TotalPrice() > 0 ? $" With your {Discount}% Discount!" : "");
    }
}