using Labb2;

var store = new Store();

if (File.Exists("products.json"))
{
    store.StoreProducts = Input.StoreProductsFromJson("products.json");
}
else
{
    //Tillfälliga produkter om fil ej finns.
    store.StoreProducts.Add(new Product(detail: "Rock", price: 2000,
        description: "Ancient polished rock. 1B Years old."));
    store.StoreProducts.Add(new Product(detail: "Stick", price: 1000,
        description: "Oak stick with a crack. 100 years old."));
    store.StoreProducts.Add(new Product(detail: "Feather", price: 500,
        description: "Feather from a magpie. Shiny black and white."));
}

var customerList = new List<string>();

//Läser in användare och skapar kunder av dessa
if (File.Exists("customers.txt"))
{
    customerList = Input.ReadListFromFile("customers.txt");
    store.Customers = Input.FixReadList(customerList);
}

//kör inloggningsmenyn
store.LogInMenu();

//Skriver användardata till fil
Input.FixWriteList(store.Customers, customerList);

//Produkter skrivs till JSON
Input.StoreProductsToJson(store.StoreProducts);