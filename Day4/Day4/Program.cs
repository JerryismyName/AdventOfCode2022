using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
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

        private class Range
        {
            public int Start { get; }
            public int End { get; }

            public Range(int start, int end)
            {
                Start = start;
                End = end;
            }

            public int OneContainsTheOther(Range other)
            {
                if (Contains(this, other))
                {
                    return -1;
                }

                if (Contains(other, this))
                {
                    return 1;
                }

                return 0;
            }

            private static bool Contains(Range one, Range other)
            {
                return one.Start <= other.Start && one.End >= other.End;
            }

            public bool OverlapsWith(Range other)
            {
                return Start <= other.End && End >= other.Start;                
            }
        }

        private static string[] GetInput()
        {
            return System.IO.File.ReadAllLines(@"input.txt");
        }

        private static bool OneIsContained(Range[] ranges)
        {
            return OneIsContained(ranges[0], ranges[1]);
        }

        private static bool OneIsContained(Range x, Range y)
        {
            return x.OneContainsTheOther(y) != 0;
        }

        private static Range GetRangeFromLine(string line)
        {
            var split = line.Split('-');
            return new Range(int.Parse(split[0]), int.Parse(split[1]));
        }

        private static Range[] GetRangesFromLine(string line)
        {
            var ranges = new Range[2];
            var split = line.Split(',');

            ranges[0] = GetRangeFromLine(split[0]);
            ranges[1] = GetRangeFromLine(split[1]);

            return ranges;
        }

        private static void Part1()
        {
            var lines = GetInput();
            var overlapCount = 0;

            foreach (var line in lines.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var ranges = GetRangesFromLine(line);
                if (OneIsContained(ranges))
                {
                    overlapCount++;
                }
            }

            Console.WriteLine($"Number of overlaps: {overlapCount}");
        }

        private static bool DoTheyOverlap(Range[] ranges)
        {
            return ranges[0].OverlapsWith(ranges[1]);
        }

        private static void Part2()
        {
            var lines = GetInput();
            var overlapCount = 0;

            foreach (var line in lines.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var ranges = GetRangesFromLine(line);
                if (DoTheyOverlap(ranges))
                {
                    overlapCount++;
                }
            }

            Console.WriteLine($"Number of overlaps: {overlapCount}");
        }
    }
}
