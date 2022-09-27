using Labb2;

//Borde inte skapa en helt ny affär för varje kund. Mängden data borde vara mindre per användare.


var store = new Store();

//bara för att göra nya produkter
//Tillfälliga produkter. Kanske inte borde ligga här i egentligen?
store.StoreProducts.Add(new Product(detail: "Rock", price: 2000, description: "Ancheint polished rock. 1B Years old."));
store.StoreProducts.Add(new Product(detail: "Stick", price: 1000, description: "Oak stick with a crack. 100 years old."));
store.StoreProducts.Add(new Product(detail: "Stick", price: 500, description: "Feather from a magpie. Shiny black and white."));

store.Customers.Add(new StandardCustomer("Knatte", "123"));
store.Customers.Add(new GoldCustomer("Fnatte", "321"));
store.Customers.Add(new BronzeCustomer("Tjatte", "213"));

//kör inloggningsmenyn
store.LogInMenu();

/*
foreach (Product prod in store.storeProducts)
{
    Console.WriteLine(prod);
}
*/
