namespace Labb2;

public class SilverCustomer : Customer
{
    private string _customerLevel;
    private int _discount = 10;

    public SilverCustomer(string name, string password) : base(name, password)
    { 
    }

    public SilverCustomer()
    {
    }


    public int Discount
    {
        get { return _discount; }
    }

}