namespace Labb2;

public class StandardCustomer : Customer
{
    private string _customerLevel;
    private int _discount = 0;

    public StandardCustomer(string name, string password) : base(name, password)
    {
    }

    public StandardCustomer()
    {
    }

    public int Discount
    {
        get { return _discount; }
    }

}