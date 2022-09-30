using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal static class Input
    {
        //Check på inmatade värden från användaren
        public static int PublicInput(int maxInput)
        {
            int choise = 0;
            bool loopCheck = false;
            Console.WriteLine($"Enter number between 1 - {maxInput}");

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
                using (StreamReader sr = new StreamReader(fileName, true))
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
    }
}
