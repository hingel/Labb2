namespace Labb2;

public class Product
{
    private string _detail;
    private double _price;
    private string _description;
    private int _quantity;

    public Product (string detail, double price, string description = "", int quantity = 5)
    {
        _detail = detail;
        _price = price;
        _description = description;
        _quantity = quantity;
    }

    public string Detail
    {
        get { return _detail; }
        set { _detail = value; }
    }

    public double Price
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

    public override string ToString()
    {
        return $"\t Detail: {Detail}. \n\t Description: {Description} \n\t Quantity: {Quantity} Price: {Price / Currency.CurrencyDouble} {Currency.CurrencyName}";
    }
}