using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace Labb2
{
    public class Customer //Skulle kunna vara abstract
    {
        private readonly string _name;
        private readonly string _password;
 
        private List<Product> _shoppingCart = new List<Product>();
        
        public Customer(string name = "hej", string password = "111")
        {
            _name = name;
            _password = password;
        }

        //public Customer () {} //För json implementeringen

        public string Name
        {
            get { return _name; }
        }

        public List<Product> ShoppingCart
        {
            get { return _shoppingCart; }
            private set { _shoppingCart = value; }
        }

        public bool CheckPassword(string input)
        {
            bool passwordCheck = false;

            if (input == _password)
            {
                passwordCheck = true;
            }

            return passwordCheck;
        }
        
        public virtual double TotalPrice()
        {
            double sumTotal = 0;

            foreach (var prod in ShoppingCart)
            {
                sumTotal += prod.Price * prod.Quantity;
            }

            return sumTotal;
        }

        public virtual void ListShoppingCart()
        {
            int count = 0;
            
            foreach (var prod in _shoppingCart)
            {
                count++;
                Console.WriteLine($"{count}. {prod} " + $"Total sum: {prod.Quantity*prod.Price}");   
            }

            Console.WriteLine($"Total price: {TotalPrice()} {Currency.CurrencyName}");
            Console.WriteLine("Press any key to continue.");
        }

        public void AddProductToCart(Product productToAdd)
        {
            var newProd = new Product(productToAdd.Detail, productToAdd.Price, productToAdd.Description, productToAdd.Quantity);
            bool addNewProd = true;

            newProd.Quantity = 1; //annars blir det tio som från orginalet
            
            foreach (var prod in ShoppingCart)
            {
                if (prod.Detail == newProd.Detail)
                {
                    prod.Quantity++;
                    addNewProd = false;
                    break;
                }
            }

            if (addNewProd)
            {
                ShoppingCart.Add(newProd);
            }
        }

        public Product RemoveProductFromCart()
        {
            if (ShoppingCart.Count > 0)
            {
                ListShoppingCart();
                int productToRemove = Input.PublicInput(ShoppingCart.Count);
                if (ShoppingCart[productToRemove - 1].Quantity > 1)
                {
                    ShoppingCart[productToRemove - 1].Quantity --;
                    return ShoppingCart[productToRemove - 1];
                }
                else
                {
                    Product tempProd = ShoppingCart[productToRemove - 1];
                    ShoppingCart.RemoveAt(productToRemove - 1);
                    return tempProd;
                }
            }
            else
            {
                Console.WriteLine("No products to remove. Press any key to continue");
                Console.ReadLine();
                return null;
            }
        }

        public void CheckOut()
        {
            ShoppingCart.Clear();
            Console.WriteLine("You pay, your shopping cart is empty. Press any key to continue");
            Console.ReadKey();
        }
        
        public string GenerateFileString()
        {
            return $"{Name} Password{_password}"; 
        }

        public override string ToString()
        {
            return $"{Name}. Total sum in Shopping Cart: {TotalPrice()} {Currency.CurrencyName}.";
        }
    }
}
