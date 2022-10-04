namespace Labb2;

public class SilverCustomer : Customer
{
    private int _discount = 10;

    public SilverCustomer(string name, string password) : base(name, password)
    { 
    }

    //public SilverCustomer(){}

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
            Console.WriteLine($"{count}. {prod} \n\t Total sum with your Discount: {prod.Quantity * prod.Price * (100 - Discount) / 100} {Currency.CurrencyName}");
        }

        Console.WriteLine($"Total price: {TotalPrice()}");
        Console.ReadLine();
    }

    public override double TotalPrice()
    {
        double sumTotal = 0;

        foreach (var prod in ShoppingCart)
        {
            sumTotal += prod.Price * prod.Quantity * (100 - Discount) / 100;
        }

        return sumTotal;
    }
    public override string ToString()
    {
        return base.ToString() + (TotalPrice() > 0 ? $" With your Discount {Discount}" : "");
    }

}