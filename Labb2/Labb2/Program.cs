using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Labb2;

//Borde inte skapa en helt ny affär för varje kund. Mängden data borde vara mindre per användare.


var store = new Store();

//bara för att göra nya produkter
//Tillfälliga produkter. Kanske inte borde ligga här i egentligen?
store.StoreProducts.Add(new Product(detail: "Rock", price: 2000, description: "Ancient polished rock. 1B Years old."));
store.StoreProducts.Add(new Product(detail: "Stick", price: 1000, description: "Oak stick with a crack. 100 years old."));
store.StoreProducts.Add(new Product(detail: "Feather", price: 500, description: "Feather from a magpie. Shiny black and white."));
store.StoreProducts.Add(new Product(detail: "Soil", price: 700, description: "Moist, fat, brown soil with traces of sand in it."));

//store.Customers.Add(new Customer("Knatte", "123"));
//store.Customers.Add(new GoldCustomer("Fnatte", "321"));
//store.Customers.Add(new BronzeCustomer("Tjatte", "213"));

//borde hämta listan med produkterna här.

var customerList = Input.ReadListFromFile("customers.txt");

foreach (var c in customerList)
{
    var splitString = c.Split(' ');
    string tempName = splitString.Skip(1).Take(splitString.Length - 2).Aggregate((str, strSum) => $"{str} {strSum}");
    string tempPassW = splitString.Last();

    if (splitString.Contains("gold"))
    {
        store.Customers.Add(new GoldCustomer(tempName, tempPassW));
    }
    else if (splitString.Contains("silver"))
    {
        store.Customers.Add(new SilverCustomer(tempName, tempPassW));
    }
    else if (splitString.Contains("bronze"))
    {
        store.Customers.Add(new BronzeCustomer(tempName, tempPassW));
    }
    else
    {
        store.Customers.Add(new Customer(tempName, tempPassW));
    }
}

//kör inloggningsmenyn
store.LogInMenu();

//skriva till fil efter utlogg
var addedCustomers = new List<string>();

foreach (var cust in store.Customers)
{
    bool add = customerList.Contains(cust.Name);
    
    //om inte finns ska då lägga till i listan
    if (!add)
    {
        if (cust is GoldCustomer)
        {
            addedCustomers.Add($"gold {cust.GenerateFileString()}");
        }
        else if (cust is SilverCustomer)
        {
            addedCustomers.Add($"silver {cust.GenerateFileString()}");
        }
        else if (cust is BronzeCustomer)
        {
            addedCustomers.Add($"bronze {cust.GenerateFileString()}");
        }
        else
        {
            addedCustomers.Add($"standard {cust.GenerateFileString()}");
        }
    }


    Input.WriteToFile(addedCustomers, "customers.txt");
}
