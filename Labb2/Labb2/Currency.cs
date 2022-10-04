namespace Labb2;

public static class Currency
{
    public static double CurrencyDouble { get; private set; } = 1;
    public static string CurrencyName { get; private set; } = "SEK";
    public static void ChooseCurrency()
    {
        Console.WriteLine("1: SEK");
        Console.WriteLine("2: EUR");
        Console.WriteLine("3: USD");

        int choise = Input.PublicInput(3);
        
        switch (choise)
        {
            case 1:
                CurrencyDouble = 1;
                CurrencyName = "SEK";
                break;
            case 2:
                CurrencyDouble = 10;
                CurrencyName = "EUR";
                break;
            case 3:
                CurrencyDouble = 10;
                CurrencyName = "USD";
                break;
            default:
                CurrencyDouble = 1;
                CurrencyName = "SEK";
                break;
        }
        
    }
}