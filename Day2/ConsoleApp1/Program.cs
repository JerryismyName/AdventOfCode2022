using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(@"Press 1 or 2 for puzzle part: ");
            var result = Console.ReadKey();
            Console.WriteLine();

            if (result.KeyChar == '1')
            {
                Part1();
            }
            else if (result.KeyChar == '2')
            {
                Part2();
            }
            else
            {
                Console.WriteLine(@"Incorrect key press");
            }

            Console.WriteLine(@"Press any key to quit.");
            Console.ReadKey();
        }
    }
}
