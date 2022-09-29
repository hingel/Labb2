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
        
        public Customer(string name = "", string password = "")
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
            private set { _shoppingCart = value; } //Kan gå direkt om inget ändras.
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
                Console.WriteLine($"{count}. {prod}");   
            }

            Console.WriteLine($"Total price: {TotalPrice()} {Currency.CurrencyName}");
            Console.ReadLine();
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

        public void RemoveProductFromCart()
        {
            if (ShoppingCart[0] != null)
            {
                ListShoppingCart();
                int productToRemove = Input.PublicInput(ShoppingCart.Count);
                ShoppingCart.RemoveAt(productToRemove - 1); //Detta funkar inte perfekt egentligen. Borde kanske hantera allt som objekt istället.
            }
            else
            {
                Console.WriteLine("No products to remove. Press any key to continue");
                Console.ReadLine();
            }
        }

        public void CheckOut()
        {
            ShoppingCart.Clear();
            Console.WriteLine("Your shopping cart is empty.Press any key to continue");
            Console.ReadLine();
        }

        public override string ToString()
        {
            return string.Format($"{Name}. Total sum in shoppingcart: {TotalPrice()} {Currency.CurrencyName}.");
        }
    }
}
