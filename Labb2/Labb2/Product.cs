namespace Labb2;

public class Product
{
    private string _detail;
    private double _price;
    private string _description;
    private int _quantity;

    public Product (string detail, double price, string description = "", int quantity = 10)
    {
        _detail = detail;
        _price = price;
        _description = description;
        _quantity = quantity;
    }

    //public Product () {} //Tom konstruktor för json implementeringen för skapande av objekt

    public string Detail
    {
        get { return _detail; }
        set { _detail = value; }
    }

    public double Price
    {
        get
        {
            return (_price / Currency.CurrencyDouble);
        } 
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
        return $"Detail: {Detail}. Description: {Description} Quantity: {Quantity} Price: {Price}";
    }
}