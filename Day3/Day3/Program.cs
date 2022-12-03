using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
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

        private static string[] GetInput()
        {
            return System.IO.File.ReadAllLines(@"input.txt");
        }

        private static int GetPriority(char c)
        {
            if (char.IsLower(c))
            {
                return c - 96;
            }

            return c - 38;
        }

        private static string[] GetCompartmentItems(string line)
        {
            var mid = line.Length / 2;
            var result = new string[2];

            result[0] = line.Substring(0, mid);
            result[1] = line.Substring(mid);

            return result;
        }

        private static IEnumerable<char> GetCommonTypes(string line)
        {
            var compartments = GetCompartmentItems(line);

            return compartments[0].Intersect(compartments[1]);
        }

        private static int ScoreLine(string line)
        {
            var commonTypes = GetCommonTypes(line).ToList();
            var score = commonTypes.Sum(GetPriority);
            
            return score;
        }

        private static void Part1()
        {
            var lines = GetInput();

            var score = lines.Sum(ScoreLine);

            Console.WriteLine($"Total score: {score}");
        }

        private static int ScoreGroup(string[] lines)
        {
            var commonTypes = lines[0].Intersect(lines[1]);
            commonTypes = string.Join(string.Empty, commonTypes).Intersect(lines[2]);

            return commonTypes.Sum(GetPriority);
        }

        private static void Part2()
        {
            var lines = GetInput();
            var firstElf = 0;
            var score = 0;

            while (firstElf < lines.Length)
            {
                score += ScoreGroup(lines.Skip(firstElf).Take(3).ToArray());
                firstElf += 3;
            }

            Console.WriteLine($"Total score: {score}");
        }
    }
}
