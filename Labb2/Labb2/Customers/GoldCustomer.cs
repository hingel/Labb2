namespace Labb2;

public class GoldCustomer : Customer
{
    private string _customerLevel;
    private int _discount = 20;

    public GoldCustomer(string name, string password) : base(name, password)
    {

    }

    public GoldCustomer() {}
    
    public int Discount
    {
        get { return _discount; }
    }
}