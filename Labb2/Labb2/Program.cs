using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Labb2;
using Labb2.Customers;

var store = new Store();

//Tillfälliga produkter. Kanske inte borde ligga här i egentligen?
store.StoreProducts.Add(new Product(detail: "Rock", price: 2000, description: "Ancient polished rock. 1B Years old."));
store.StoreProducts.Add(new Product(detail: "Stick", price: 1000, description: "Oak stick with a crack. 100 years old."));
store.StoreProducts.Add(new Product(detail: "Feather", price: 500, description: "Feather from a magpie. Shiny black and white."));
store.StoreProducts.Add(new Product(detail: "Soil", price: 700, description: "Moist, fat, brown soil with traces of sand in it."));

//Läser in användare och skapar objekt
var customerList = Input.ReadListFromFile("customers.txt");
store.Customers = Input.FixReadList(customerList);

//kör inloggningsmenyn
store.LogInMenu();

//Skriver användardata till fil
Input.FixWriteList(store.Customers, customerList);


