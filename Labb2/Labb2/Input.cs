using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal static class Input
    {
        //Inmatade värden från användaren
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

    }
}
