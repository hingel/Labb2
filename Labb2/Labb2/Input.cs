using Labb2.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace Labb2
{
    internal static class Input
    {
        //Check på inmatade värden från användaren
        public static int PublicInput(int maxInput)
        {
            int choise = 0;
            bool loopCheck = false;
            Console.WriteLine($"Enter number between 1 - {maxInput} and press Enter.");

            while (!loopCheck)
            {
                loopCheck = int.TryParse(Console.ReadLine(), out choise);

                if (!loopCheck)
                    Console.WriteLine($"Not a number. Enter number between 1 - {maxInput}");

                else if (choise < 1)
                {
                    Console.WriteLine("Enter number larger than: 0");
                    loopCheck = false;
                }

                else if (choise > maxInput)
                {
                    Console.WriteLine($"To large number. Max: {maxInput}");
                    loopCheck = false;
                }
            }
            return choise;
        }

        public static List<string> ReadListFromFile(string fileName)
        {
            var list = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line = string.Empty;

                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return list;
        }

        public static void WriteToFile(List<string> list, string filename)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    foreach (var l in list)
                    {
                        sw.WriteLine(l);
                    }

                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static List<Customer> FixReadList(List<string> listToFix)
        {
            var custListFixed = new List<Customer>();

            foreach (var c in listToFix)
            {
                var splitString = c.Split(' ');
                //För att sortera ut användarnamnet, ok att det innehåller mellanslag
                string tempName = splitString.Skip(1)
                    .TakeWhile(str => !str.Contains("Password"))
                    .Aggregate((str, strSum) => $"{str} {strSum}");
                //För att sortera ut lösenordet, ok att det innehåller mellanslag
                string tempPassW = splitString.SkipWhile(str => !str.Contains("Password"))
                    .Take(splitString.Length).Aggregate((strsum, str) => $"{strsum} {str}")
                    .Substring(8);

                if (splitString.Contains("gold"))
                {
                    custListFixed.Add(new GoldCustomer(tempName, tempPassW));
                }
                else if (splitString.Contains("silver"))
                {
                    custListFixed.Add(new SilverCustomer(tempName, tempPassW));
                }
                else if (splitString.Contains("bronze"))
                {
                    custListFixed.Add(new BronzeCustomer(tempName, tempPassW));
                }
                else if (splitString.Contains("admin"))
                {
                    custListFixed.Add(new AdminUser(tempName, tempPassW));
                }
                else
                {
                    custListFixed.Add(new Customer(tempName, tempPassW));
                }
            }

            return custListFixed;
        }

        public static void FixWriteList(List<Customer> custList, List<string> customerList)
        {
            var addedCustomers = new List<string>();

            foreach (var cust in custList)
            {
                bool add = customerList.Contains(cust.Name); //om användaren redan finns ska den inte läggas till

                if (!add)
                {
                    if (cust is GoldCustomer)
                    {
                        addedCustomers.Add($"gold {cust.GenerateFileString()}");
                    }
                    else if (cust is SilverCustomer)
                    {
                        addedCustomers.Add($"silver {cust.GenerateFileString()}");
                    }
                    else if (cust is BronzeCustomer)
                    {
                        addedCustomers.Add($"bronze {cust.GenerateFileString()}");
                    }
                    else if (cust is AdminUser)
                    {
                        addedCustomers.Add($"admin {cust.GenerateFileString()}");
                    }
                    else
                    {
                        addedCustomers.Add($"standard {cust.GenerateFileString()}");
                    }
                }

                WriteToFile(addedCustomers, "customers.txt");
            }
        }

        public static void StoreProductsToJson(List<Product> productList)
        {
            var jSONList = new List<string>();

            foreach (var prod in productList)
            {
                jSONList.Add(JsonSerializer.Serialize(prod));
            }

            WriteToFile(jSONList, "products.json");
        }

        public static List<Product> StoreProductsFromJson(string fileName)
        {
            var prodList = new List<Product>();

            var jSonList = ReadListFromFile(fileName);

            foreach (var str in jSonList)
            {
                Product prod = JsonSerializer.Deserialize<Product>(str);

                prod.Quantity = 10; //för att detaljer inte ska försvinna om en användare inte checkar ut.

                prodList.Add(prod);
            }

            return prodList;
        }
    }
}
