using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;

namespace Labb2
{
    internal class Store
    {
        private List<Product> _storeProducts = new List<Product>();
        private List<Customer> _customers = new List<Customer>();
        private int _logedInCustomer = -1;

        public enum Currency { Sek = 1, Eur = 2, Usd = 5 };


        public List<Product> StoreProducts
        {
            get { return _storeProducts; }
            set { _storeProducts = value; }
        }

        public List<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value; }
        }

        public void LogInMenu()
        {
            bool quit = false;
            Currency chosenCurrency = Currency.Eur;

            while (!quit)
            {
                Console.WriteLine("1. Login:");
                Console.WriteLine("2. New Customer:");
                Console.WriteLine("3. Choose Currency:");
                Console.WriteLine("4. Quit");

                int menuChoise = Input.PublicInput(4);

                switch (menuChoise)
                {
                    case 1:
                        Console.WriteLine("I log in första menyn");
                        LogIn();
                        break;
                    case 2:
                        Console.WriteLine("I new customer, första menyn");
                        NewCustomer();
                        break;
                    case 3:
                        Labb2.Currency.ChooseCurrency();
                        break;
                    case 4:
                        Console.WriteLine("I quit menyn första menyn");
                        quit = true;
                        break;
                }
            } 
        }

        public void ShoppingMenu()
        {
            bool quit = false;
            
            while (!quit)
            {
                Console.Clear();

                Console.WriteLine($"Welcome {Customers[_logedInCustomer]}");

                Console.WriteLine();

                Console.WriteLine("1. Buy Product:");
                Console.WriteLine("2. Remove Product:");
                Console.WriteLine("3. List shopping car");
                Console.WriteLine("4. Check out");
                Console.WriteLine("5. Log Out");

                int choise = Input.PublicInput(5);

                switch (choise)
                {
                    case 1: //Köpa produkt
                        Console.WriteLine("What product to buy? Enter number to chose");
                        ListStoreProducts();
                        int prodToAdd = Input.PublicInput(StoreProducts.Count);
                        Customers[_logedInCustomer].AddProductToCart(StoreProducts[prodToAdd-1]);
                        StoreProducts[prodToAdd - 1].Quantity -= 1;
                        break;
                    case 2: //Ta bort produkt
                        Customers[_logedInCustomer].RemoveProductFromCart();
                        //Borde lägga tillbaka den i kassan igen.
                        break;
                    case 3://Lista shopping cart
                        Customers[_logedInCustomer].ListShoppingCart();
                        break;
                    case 4: //checka ut och betala
                        Customers[_logedInCustomer].CheckOut();
                        break;
                    case 5: //Logout
                        Console.WriteLine("log out");
                        LogOut();
                        quit = true;
                        break;
                }
            }
        }

        public void NewCustomer()
        {
            bool loopCheck = false;
            string tempName = string.Empty;

            Console.Clear();
            
            do
            {
                loopCheck = false;
                Console.WriteLine("Enter Username:");
                tempName = Console.ReadLine(); //Borde ta bort alla mellanslag på slutet om de existerar, eller annan formatering.

                if (tempName == "")
                {
                    Console.WriteLine("Name cannot be blank");
                    loopCheck = true;
                }

                //Checka först om användaren redan finns i filläsaren
                foreach (var cust in Customers)
                {
                    //Borde väl gå att använda någon contain funktion istället?
                    if (cust.Name.ToLower() == tempName.ToLower())
                    {
                        Console.WriteLine($"{tempName} is not available. Enter another Username:");
                        loopCheck = true;
                        break;
                    }
                }
            } while (loopCheck);

            
            Console.WriteLine("Enter a 3 letter/number password:");
            string tempPassword = Console.ReadLine();

            var customer1 = new Customer(name: tempName, password: tempPassword);

            Customers.Add(customer1);

            _logedInCustomer = Customers.Count - 1;

            ShoppingMenu();

        }

        public void ListStoreProducts()
        {
            for (int i = 0; i < StoreProducts.Count; i++)
            {
                Console.WriteLine("{0} {1}", i+1, StoreProducts[i].ToString());
            }
        }

        //Byter aktiv användare i listan
        public void LogIn()
        {
            bool loopCheck = false;
            bool passwordCheck = false;
            string tempName = string.Empty;

            Console.WriteLine("Enter Username:");

            do
            {
                tempName = Console.ReadLine(); //Borde ta bort alla mellanslag på slutet om de existerar, eller annan formatering.
                //Checka först om användaren redan finns i filläsaren
                for (int i = 0; i < Customers.Count; i++)
                {
                    
                    
                    //Borde väl gå att använda någon contain funktion istället? //Kör med Linq istället.
                    if (Customers[i].Name.ToLower() == tempName.ToLower())
                    {
                        Console.WriteLine("Enter password: \t or ESC to quit.");
                        passwordCheck = Customers[i].CheckPassword(Console.ReadLine()); //Borde göra en check på lösenordet innan det skrivs in. Om det är för långt eller liknand

                        if (passwordCheck)
                        {
                            _logedInCustomer = i;
                            loopCheck = true;
                        }
                        /*else if (Console.ReadKey().Key == ConsoleKey.Escape)
                        {
                            loopCheck = true;
                            break;
                        }*/
                        else
                        {
                            
                            Console.Clear();
                            Console.WriteLine("Wrong password. Enter again:");
                            //Kan lägga till en räknare om användaren skriver in fellösenord för många ggr.
                            //Även att trycka en knapp för att breaka
                            
                        }
                        
                    }
                }

                if (!loopCheck)
                {
                    Console.WriteLine("No user found with that name:");
                    break;
                }

            } while (!loopCheck);

            if (passwordCheck)
            {
                Console.WriteLine($"Current customer {_logedInCustomer}");
                ShoppingMenu();
            }
        }

        public void LogOut()
        {
            _logedInCustomer = -1; 
        }

    }
}
