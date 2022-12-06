using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
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
                Part(4);
            }
            else if (result.KeyChar == '2')
            {
                Part(14);
            }
            else
            {
                Console.WriteLine(@"Incorrect key press");
            }

            Console.WriteLine(@"Press any key to quit.");
            Console.ReadKey();
        }

        private static string[] GetInput()
        {
            return System.IO.File.ReadAllLines(@"input.txt");
        }

        private static void Part(int markerLength)
        {
            var data = GetInput()[0];

            var queue = new List<char>();
            var index = 0;

            foreach (var c in data)
            {
                index++;
                while (queue.Contains(c))
                {
                    queue.RemoveAt(0);
                }

                queue.Add(c);
                if (queue.Count() >= 4)
                {
                    break;
                }
            }

            Console.WriteLine($"End of marker index: {index}");
        }
    }
}
