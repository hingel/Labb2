namespace Labb2;

public class Product
{
    private string _detail;
    private int _price;
    private string _description;
    private int _quantity;

    public Product (string detail = "", int price = 0, string description = "", int quantity = 0)
    {
        _detail = detail;
        _price = price;
        _description = description;
        _quantity = quantity;
    }

    public Product () {} //Tom konstruktor för json implementeringen för skapande av objekt

    public string Detail
    {
        get { return _detail; }
        set { _detail = value; }
    }

    public int Price
    {
        get { return _price; }
        set { _price = value; }
    }

    public string Description
    {
        get { return _description;}
        set { _description = value; }
    }

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    //Här nere borde priset kanske visas egentligen för varje grej. Men hur lösa det på ett bra sätt?
    public override string ToString()
    {
        return string.Format("{0} Price: {1} \t Description: {2}", Detail, Price, Description );
    }
}