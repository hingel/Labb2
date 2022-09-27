using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    public class Customer //Skulle kunna vara abstract
    {
        private readonly string _name;
        private readonly string _password;
        private string _currency = "SEK"; //Kanske göra den till en enum?

        //Uppdatera denna med rätt access nivå med Property. Ska vara 
        private List<Product> _shoppingCart = new List<Product>();
        
        public Customer(string name, string password)
        {
            _name = name;
            _password = password;
        }

        public Customer () {} //För json implementeringen

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

        //Metod för att beräkna totalpris. Ärvs av varje användare. Ska bero på valutan. //denna kan vara en abstract
        //Eller en virtuell metod också
        public int TotalPrice()
        {
            int sumTotal = 0;

            //Loop som går igenom hela listan med objekt

            return sumTotal;
        }

        public void ListShoppingCart()
        {
            //skriv ut hela listan med produkter.
            //Kan göra en TO-string från producterna

            foreach (var prod in _shoppingCart)
            {
                Console.WriteLine(prod);   
            }
        }

        public void AddProductToCart(Product productToAdd) //Vartifrån ska denna menyn nås? Från store.
        {
            //Måste kolla om detaljen redan finns. Isf lägga till en quantitet.
            _shoppingCart.Add(productToAdd);
        }

        public void RemoveProductFromCart()
        {
            int productToRemove = 0;
            //lista upp varukorgen och välj med siffertangent vad som ska tas bort.
            ListShoppingCart();
            _shoppingCart.RemoveAt(productToRemove - 1); 
        }

        //TO string metod vad ska den visa mer? Hur göra med valutan
        public override string ToString()
        {
            return string.Format("{0}. Total sum in shoppingcart: {1}. {2} ", Name, TotalPrice(), _currency);
        }
    }
}
