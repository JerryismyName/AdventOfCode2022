using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
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

        private static char GetCharAtPos(string line, int pos)
        {
            return line.Substring((pos * 4) + 1, 1)[0];
        }

        private static Stack<char>[] MapLines(string[] lines)
        {
            var labelLine = lines.Last(l => !string.IsNullOrWhiteSpace(l));
            var lastStack = int.Parse(labelLine.Substring(labelLine.Length - 2, 1));
            var reversedLines = lines.Take(Array.IndexOf(lines, labelLine)).Reverse();
            var map = new Stack<char>[lastStack];
            for (var i = 0; i < map.Length; i++)
            {
                map[i] = new Stack<char>();
            }

            foreach(var line in reversedLines)
            {
                for (var i = 0; i < lastStack; i++)
                {
                    var item = GetCharAtPos(line, i);
                    
                    if (!char.IsWhiteSpace(item))
                    {
                        map[i].Push(item);
                    }
                }
            }

            return map;
        }

        private static void ExecuteMove1(string line, Stack<char>[] map)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            var move = new Move(line);

            for (int count = 0; count < move.Count; count++)
            {
                if (map[move.From].Count < 1)
                {
                    return;
                }

                var item = map[move.From].Pop();
                map[move.To].Push(item);
            }
        }

        private static void ExecuteMove2(string line, Stack<char>[] map)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            var move = new Move(line);
            var tempStack = new Stack<char>();
            
            for (int count = 0; count < move.Count; count++)
            {
                if (map[move.From].Count < 1)
                {
                    return;
                }

                var item = map[move.From].Pop();
                tempStack.Push(item);
            }

            while (tempStack.Count > 0)
            {
                map[move.To].Push(tempStack.Pop());
            }
        }

        private static string GetTopItems(Stack<char>[] map)
        {
            var builder = new StringBuilder();

            foreach (var stack in map)
            {
                builder.Append(stack.Peek());
            }

            return builder.ToString();
        }
        
        private static void Part1()
        {
            var lines = GetInput();

            var blankLine = lines.First(l => string.IsNullOrWhiteSpace(l));
            var blankLineIndex = Array.IndexOf(lines, blankLine);
            var map = MapLines(lines.Take(blankLineIndex).ToArray());

            foreach (var line in lines.Skip(blankLineIndex))
            {
                ExecuteMove1(line, map);
            }

            Console.WriteLine($"Top items: {GetTopItems(map)}");
        }

        private static void Part2()
        {
            var lines = GetInput();

            var blankLine = lines.First(l => string.IsNullOrWhiteSpace(l));
            var blankLineIndex = Array.IndexOf(lines, blankLine);
            var map = MapLines(lines.Take(blankLineIndex).ToArray());

            foreach (var line in lines.Skip(blankLineIndex))
            {
                ExecuteMove2(line, map);
            }

            Console.WriteLine($"Top items: {GetTopItems(map)}");
        }

        private class Move
        {
            public int Count { get; }
            public int From { get; }
            public int To { get; }

            public Move(string line)
            {
                var split = line.Split(' ');
                Count = int.Parse(split[1]);
                From = int.Parse(split[3]) - 1;
                To = int.Parse(split[5]) - 1;
            }
        }
    }
}
