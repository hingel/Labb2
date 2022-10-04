using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using Labb2.Customers;

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
        public void LogIn()
        {
            bool loopBreak = false;
            bool passwordCheck = false;
            bool customerFound = false;
            var tempName = string.Empty;
            var testPassword = string.Empty;

            while (!loopBreak)
            {
                Console.WriteLine("Enter Username or press Enter to quit.:");
                tempName = Console.ReadLine();
                Console.WriteLine("Enter password or press Enter to quit.");
                testPassword = Console.ReadLine();

                if (testPassword == "" || tempName == "")
                {
                    break;
                }

                _logedInCustomer = Customers.FindIndex(cust => cust.Name.ToLower() == tempName.ToLower());

                if (_logedInCustomer >= 0)
                {
                    customerFound = true;
                    passwordCheck = Customers[_logedInCustomer].CheckPassword(testPassword);

                    if (passwordCheck)
                    {
                        loopBreak = true;
                    }
                    else
                    {
                        Console.WriteLine("Wrong password.");
                        //Kan lägga till en räknare om användaren skriver in fellösenord för många ggr.
                    }
                }
                else
                {
                    Console.WriteLine("Customer not found.");
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
        public void NewCustomer()
        {
            bool loopCheck = true;
            var tempName = string.Empty;
            var tempPassword = string.Empty;
            Console.Clear();

            do
            {
                Console.WriteLine("Enter Username or press Enter to quit:");
                loopCheck = false;
                tempName = Console.ReadLine();

                Console.WriteLine("Enter a password or press Enter to quit:");
                tempPassword = Console.ReadLine();

                if (tempName == "" || tempPassword == "")
                {
                    break;
                }
                
                if (Customers.Count(c => c.Name.ToLower() == tempName.ToLower()) > 0 || tempName.ToLower() == "password") //Inte det snyggaste borde kunna få en bool direkt
                {
                    Console.WriteLine($"{tempName} is not available. Enter a new Username:");
                    loopCheck = true;
                }

            } while (loopCheck);

            if (tempName != "" && tempPassword != "")
            {
                var customerNew = new Customer(name: tempName, password: tempPassword);
                Customers.Add(customerNew);
                _logedInCustomer = Customers.Count - 1;
                ShoppingMenu();
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
                Console.WriteLine("2. Remove Product from Shopping Cart:");
                Console.WriteLine("3. List Shopping Cart");
                Console.WriteLine("4. Check out");
                Console.WriteLine("5. Log Out");

                int noOfChoices = 5;
                
                //Här ha villkor om admin user:
                if (Customers[_logedInCustomer] is AdminUser)
                {
                    Console.WriteLine("6. Admin Menu: Add product to store.");
                    noOfChoices = 6;
                }

                int choise = Input.PublicInput(noOfChoices);

                switch (choise)
                {
                    case 1: //Köpa produkt
                        BuyProduct();
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
                        Console.ReadKey();
                        break;
                    case 4: //checka ut och betala
                        Console.Clear();
                        Console.WriteLine("Shopping Cart");
                        Customers[_logedInCustomer].CheckOut();
                        break;
                    case 5: //Logout
                        Console.WriteLine("log out");
                        LogOut();
                        quit = true;
                        break;
                    case 6:
                        if (Customers[_logedInCustomer] is AdminUser admin)
                        {
                            StoreProducts.Add(admin.AddNewProductToStore());
                        }
                        break;
                }
            }
        }

        public void BuyProduct()
        {
            Console.Clear();
            Console.WriteLine("What product to buy?");
            ListStoreProducts();
            int prodToAdd = Input.PublicInput(StoreProducts.Count);
            //Om antalet är mindre än ett i butiken
            if (StoreProducts[prodToAdd - 1].Quantity < 1)
            {
                Console.WriteLine($"{StoreProducts[prodToAdd - 1].Detail} is not available. Press any key to continue.");
                Console.ReadKey();
            }
            else
            {
                Customers[_logedInCustomer].AddProductToCart(StoreProducts[prodToAdd - 1]);
                StoreProducts[prodToAdd - 1].Quantity -= 1;
            }
        }
        
        public void ListStoreProducts()
        {
            for (int i = 0; i < StoreProducts.Count; i++)
            {
                Console.WriteLine($"{i+1}. {StoreProducts[i].ToString()}");
            }
        }

        public void LogOut()
        {
            _logedInCustomer = -1;  
        }
    }
}
