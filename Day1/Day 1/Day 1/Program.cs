using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_1
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

        private static IEnumerable<string> GetInput()
        {
            return System.IO.File.ReadAllLines(@"input.txt");
        }

        private static void Part1()
        {
            var lines = GetInput();
            var currentCalories = 0;
            var mostCalories = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    currentCalories = 0;
                    continue;
                }

                if (!int.TryParse(line, out var calories))
                {
                    continue;
                }

                currentCalories += calories;
                if (currentCalories > mostCalories)
                {
                    mostCalories = currentCalories;
                }
            }

            Console.WriteLine($"Most calories held by an elf: {mostCalories}");
        }

        private static void Part2()
        {
            var lines = GetInput();
            var topCounts = new int[3];
            var lowestPosition = 0;
            var lowestCalories = 0;
            var currentCalories = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentCalories > lowestCalories)
                    {
                        topCounts[lowestPosition] = currentCalories;
                        lowestCalories = topCounts.Min();
                        lowestPosition = Array.IndexOf(topCounts, lowestCalories);
                    }

                    currentCalories = 0;
                    continue;
                }

                if (!int.TryParse(line, out var calories))
                {
                    continue;
                }

                currentCalories += calories;
            }

            Console.WriteLine($"Top 3 calorie counts: {string.Join(", ", topCounts)}");
            Console.WriteLine($"Total of top 3 calorie counts: {topCounts.Sum()}");
        }
    }
}
