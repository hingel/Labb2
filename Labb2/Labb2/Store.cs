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
                        LogIn();
                        break;
                    case 2:
                        NewCustomer();
                        break;
                    case 3:
                        Currency.ChooseCurrency();
                        break;
                    case 4:
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
                        Console.Clear();
                        Console.WriteLine("What product to buy? Enter number to chose");
                        ListStoreProducts();
                        int prodToAdd = Input.PublicInput(StoreProducts.Count);
                        Customers[_logedInCustomer].AddProductToCart(StoreProducts[prodToAdd-1]);
                        StoreProducts[prodToAdd - 1].Quantity -= 1;
                        break;
                    case 2: //Ta bort produkt
                        Console.Clear();
                        Console.WriteLine("What product to remove?");
                        Product tempProd = Customers[_logedInCustomer].RemoveProductFromCart();

                        //Lägger tillbaks produkten i kassan
                        if (tempProd != null)
                        {
                            StoreProducts.Single(p => p.Detail == tempProd.Detail).Quantity++;
                        }
                        break;
                    case 3://Lista shopping cart
                        Console.Clear();
                        Console.WriteLine("Shopping Cart");
                        Customers[_logedInCustomer].ListShoppingCart();
                        Console.ReadLine();
                        break;
                    case 4: //checka ut och betala
                        Console.Clear();
                        Console.WriteLine("Shopping Cart");
                        Customers[_logedInCustomer].CheckOut();
                        Console.ReadLine();
                        break;
                    case 5: //Logout
                        Console.WriteLine("log out");
                        LogOut();
                        quit = true;
                        Console.ReadLine();
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
                tempName = Console.ReadLine();

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

            
            Console.WriteLine("Enter a password:");
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
                Console.WriteLine($"{i+1} {StoreProducts[i].ToString()} {Currency.CurrencyName}");
            }
        }

        //Byter aktiv användare i listan
        public void LogIn()
        {
            bool loopBreak = false;
            bool passwordCheck = false;
            bool customerFound = false;
            string tempName = string.Empty;
            string testPassword = string.Empty;
            
            while (!loopBreak)
            {

                Console.WriteLine("Enter Username:");
                tempName = Console.ReadLine();
                Console.WriteLine("Enter password or press Enter to quit.");
                testPassword = Console.ReadLine();

                if (testPassword == "")
                {
                    break;
                }
                //Checka först om användaren redan finns i filläsaren //Borde väl gå att använda någon contain funktion istället? //Kör med Linq istället.
                for (int i = 0; i < Customers.Count; i++)
                {
                    if (Customers[i].Name.ToLower() == tempName.ToLower())
                    {
                        customerFound = true;
                        passwordCheck = Customers[i].CheckPassword(testPassword); //Borde göra en check på lösenordet innan det skrivs in. Om det är för långt eller liknand
                        
                        if (passwordCheck)
                        {
                            _logedInCustomer = i;
                            loopBreak = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong password. Enter again:");
                            //Kan lägga till en räknare om användaren skriver in fellösenord för många ggr.
                        }
                    }
                }

                if (!customerFound)
                {
                    Console.WriteLine("User does not exist. Press: \n 1. to create new Customer. \n 2. or Enter to quit.");

                    if (Console.ReadLine() == "1")
                    {
                        NewCustomer();
                        break;
                    }
                    else
                    {
                        break;
                    }
                } 
            }

            if (passwordCheck)
            {
                Console.WriteLine($"Current customer {_logedInCustomer}");
                ShoppingMenu();
            }
        }

        public void LogOut()
        {
            //här en metod för att skicka användaren till textfilen.
            
            _logedInCustomer = -1;  
        }

    }
}
