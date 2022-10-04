using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Labb2.Customers
{
    internal class AdminUser : Customer
    {
        public AdminUser(string name, string password) : base(name, password)
        {

        }

        public Product AddNewProductToStore()
        {
            Console.WriteLine("Add new product to store.");
            Console.WriteLine("Enter detail name:");
            string newDetail = Console.ReadLine();
            Console.WriteLine("Enter price for detail: (SEK)");
            double newPrice = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter description:");
            string newDescription = Console.ReadLine();
            
            var newAddedProduct = new Product(newDetail, newPrice, newDescription);

            return newAddedProduct;
        }

        //Bygg ut med funktioner för att ta bort detaljer ur listan.

    }

}
