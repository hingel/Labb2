namespace Labb2;

public class BronzeCustomer : Customer
{
    private string _customerLevel;
    private int _discount = 5;

    public BronzeCustomer(string name, string password) : base(name, password)
    {
    }

    public BronzeCustomer() {}

    public int Discount
    {
        get { return _discount; }
    }
}